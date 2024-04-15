using Microsoft.EntityFrameworkCore;
using SimvestFun.ApplicationCore.Entities;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using SimvestFun.ApplicationCore.Interfaces;
using SimvestFun.ApplicationCore.Jobs;
using SimvestFun.ApplicationCore.Services;
using SimvestFun.Infrastructure;
using System.Reflection;
using System.Text.Json.Serialization;

var CorsAllowedOrigins = "_CORSAllowedOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: CorsAllowedOrigins,
        builder =>
        {
            builder.WithOrigins("http://localhost:4200");
            builder.AllowAnyHeader();
            builder.AllowAnyMethod();
        });
});
var connection = builder.Configuration.GetConnectionString("SimvestFunContext");
builder.Services.AddDbContext<SimvestFunContext>(options => options.UseSqlServer(connection, builder => builder.EnableRetryOnFailure()));

builder.Services.AddIdentityCore<ApplicationUser>()
    .AddEntityFrameworkStores<SimvestFunContext>();

builder.Services.AddScoped<ISimvestFunContext>(provider => provider.GetRequiredService<SimvestFunContext>());
builder.Services.AddScoped<IFinnhubAPIService, FinnhubAPIService>();
builder.Services.AddScoped<IStockService, StockService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPortfolioValuesService, PortfolioValuesService>();
builder.Services.AddScoped<IUserStockService, UserStockService>();
builder.Services.AddScoped<IUserActionService, UserActionService>();
builder.Services.AddScoped<IStockPriceService, StockPriceService>();
builder.Services.AddScoped<IJwtUtils, JwtUtils>();
builder.Services.AddScoped<IPricesJobService, PricesJobService>();
builder.Services.AddScoped<IFollowService, FollowService>();
builder.Services.AddScoped<ISettingService, SettingService>();

builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(AutoMapperProfile)));
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

builder.Services.AddHostedService<AppHostedService>();
builder.Services.AddSingleton<IJobFactory, SingletonJobFactory>();
builder.Services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

var timeofPricesJob = int.Parse(builder.Configuration.GetSection("JobTiming:TimeinMinutes").Value);
ITrigger pricesJobTrigger = TriggerBuilder.Create()
                .WithIdentity("Trigger1", "MyJobs")
                .StartNow()
                .WithSimpleSchedule(x => x.WithIntervalInMinutes(timeofPricesJob).RepeatForever())
                .Build();

builder.Services.AddSingleton<PricesJob>();
builder.Services.AddSingleton(new Job(type: typeof(PricesJob), trigger: pricesJobTrigger));

ITrigger rememberUsJobTrigger = TriggerBuilder.Create()
                .WithIdentity("Trigger2", "MyJobs")
                .WithCronSchedule("0 0 14 ? * MON-FRI", x => x.InTimeZone(TimeZoneInfo.Utc))
                .Build();

builder.Services.AddSingleton<RememberUsJob>();
builder.Services.AddSingleton(new Job(type: typeof(RememberUsJob), trigger: rememberUsJobTrigger));

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.Configure<IISOptions>(options =>
{
    options.AutomaticAuthentication = false;
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var simvestFunContext = scope.ServiceProvider.GetRequiredService<SimvestFunContext>();
    simvestFunContext.Database.Migrate();
    await SeedService.SeedData(simvestFunContext);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseCors(CorsAllowedOrigins);

app.UseAuthorization();

app.UseMiddleware<JwtMiddleware>();

app.MapControllers();

app.MapFallbackToFile("index.html");

app.Run();
