using Microsoft.AspNetCore.Http;
using SimvestFun.ApplicationCore.Interfaces;

namespace SimvestFun.Infrastructure
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserService userService, IJwtUtils jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = jwtUtils.ValidateToken(token);
            if(userId != null)
                context.Items["User"] = await userService.GetById(userId);
            await _next(context);
        }
    }
}
