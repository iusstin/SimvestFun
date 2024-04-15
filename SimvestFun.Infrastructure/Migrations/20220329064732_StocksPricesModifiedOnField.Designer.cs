﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SimvestFun.Infrastructure;

#nullable disable

namespace SimvestFun.Infrastructure.Migrations
{
    [DbContext(typeof(SimvestFunContext))]
    [Migration("20220329064732_StocksPricesModifiedOnField")]
    partial class StocksPricesModifiedOnField
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("SimvestFun.ApplicationCore.Entities.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("SimvestFun.ApplicationCore.Entities.Stock", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("CurrentPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Index")
                        .HasColumnType("int");

                    b.Property<string>("Industry")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PriceUpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Stocks");

                    b.HasData(
                        new
                        {
                            Id = "AAPL",
                            CurrentPrice = 162.95m,
                            Index = 1,
                            Industry = "Consumer electronics, software and online services",
                            Name = "Apple",
                            PriceUpdatedOn = new DateTime(2022, 3, 29, 6, 47, 32, 63, DateTimeKind.Utc).AddTicks(2641)
                        },
                        new
                        {
                            Id = "MSFT",
                            CurrentPrice = 288.50m,
                            Index = 2,
                            Industry = "Multinational technology",
                            Name = "Microsoft",
                            PriceUpdatedOn = new DateTime(2022, 3, 29, 6, 47, 32, 63, DateTimeKind.Utc).AddTicks(2646)
                        },
                        new
                        {
                            Id = "GOOG",
                            CurrentPrice = 2677m,
                            Index = 3,
                            Industry = "Conglomerate",
                            Name = "Alphabet",
                            PriceUpdatedOn = new DateTime(2022, 3, 29, 6, 47, 32, 63, DateTimeKind.Utc).AddTicks(2647)
                        },
                        new
                        {
                            Id = "AMZN",
                            CurrentPrice = 2786m,
                            Index = 4,
                            Industry = "E-commerce",
                            Name = "Amazon",
                            PriceUpdatedOn = new DateTime(2022, 3, 29, 6, 47, 32, 63, DateTimeKind.Utc).AddTicks(2648)
                        },
                        new
                        {
                            Id = "TSLA",
                            CurrentPrice = 858.97m,
                            Index = 5,
                            Industry = "Electric vehicle and clean energy",
                            Name = "Tesla",
                            PriceUpdatedOn = new DateTime(2022, 3, 29, 6, 47, 32, 63, DateTimeKind.Utc).AddTicks(2649)
                        },
                        new
                        {
                            Id = "BRK.B",
                            CurrentPrice = 488245m,
                            Index = 6,
                            Industry = "Financial services",
                            Name = "Berkshire Hathaway",
                            PriceUpdatedOn = new DateTime(2022, 3, 29, 6, 47, 32, 63, DateTimeKind.Utc).AddTicks(2649)
                        },
                        new
                        {
                            Id = "NVDA",
                            CurrentPrice = 230.14m,
                            Index = 7,
                            Industry = "Visual computing",
                            Name = "Nvidia",
                            PriceUpdatedOn = new DateTime(2022, 3, 29, 6, 47, 32, 63, DateTimeKind.Utc).AddTicks(2650)
                        },
                        new
                        {
                            Id = "FB",
                            CurrentPrice = 198.50m,
                            Index = 8,
                            Industry = "Information technology",
                            Name = "Meta",
                            PriceUpdatedOn = new DateTime(2022, 3, 29, 6, 47, 32, 63, DateTimeKind.Utc).AddTicks(2650)
                        },
                        new
                        {
                            Id = "UNH",
                            CurrentPrice = 485.57m,
                            Index = 9,
                            Industry = "Managed healthcare insurance",
                            Name = "United Health",
                            PriceUpdatedOn = new DateTime(2022, 3, 29, 6, 47, 32, 63, DateTimeKind.Utc).AddTicks(2651)
                        },
                        new
                        {
                            Id = "JNJ",
                            CurrentPrice = 169.36m,
                            Index = 10,
                            Industry = "Pharmaceutical Medical devices Consumer healthcare",
                            Name = "Johnson & Johnson",
                            PriceUpdatedOn = new DateTime(2022, 3, 29, 6, 47, 32, 63, DateTimeKind.Utc).AddTicks(2652)
                        },
                        new
                        {
                            Id = "V",
                            CurrentPrice = 199.76m,
                            Index = 11,
                            Industry = "Financial services",
                            Name = "Visa",
                            PriceUpdatedOn = new DateTime(2022, 3, 29, 6, 47, 32, 63, DateTimeKind.Utc).AddTicks(2652)
                        },
                        new
                        {
                            Id = "JPM",
                            CurrentPrice = 133.44m,
                            Index = 12,
                            Industry = "Financial services",
                            Name = "JPMorgan Chase",
                            PriceUpdatedOn = new DateTime(2022, 3, 29, 6, 47, 32, 63, DateTimeKind.Utc).AddTicks(2653)
                        },
                        new
                        {
                            Id = "WMT",
                            CurrentPrice = 139.46m,
                            Index = 13,
                            Industry = "Retail",
                            Name = "Walmart",
                            PriceUpdatedOn = new DateTime(2022, 3, 29, 6, 47, 32, 63, DateTimeKind.Utc).AddTicks(2654)
                        },
                        new
                        {
                            Id = "PG",
                            CurrentPrice = 148.77m,
                            Index = 14,
                            Industry = "Consumer goods",
                            Name = "Procter & Gamble",
                            PriceUpdatedOn = new DateTime(2022, 3, 29, 6, 47, 32, 63, DateTimeKind.Utc).AddTicks(2654)
                        },
                        new
                        {
                            Id = "XOM",
                            CurrentPrice = 82.79m,
                            Index = 15,
                            Industry = "Oil and gas",
                            Name = "Exxon Mobil",
                            PriceUpdatedOn = new DateTime(2022, 3, 29, 6, 47, 32, 63, DateTimeKind.Utc).AddTicks(2655)
                        },
                        new
                        {
                            Id = "HD",
                            CurrentPrice = 317.20m,
                            Index = 16,
                            Industry = "Retail",
                            Name = "Home Depot",
                            PriceUpdatedOn = new DateTime(2022, 3, 29, 6, 47, 32, 63, DateTimeKind.Utc).AddTicks(2656)
                        },
                        new
                        {
                            Id = "BAC",
                            CurrentPrice = 41.04m,
                            Index = 17,
                            Industry = "Financial services",
                            Name = "Bank of America",
                            PriceUpdatedOn = new DateTime(2022, 3, 29, 6, 47, 32, 63, DateTimeKind.Utc).AddTicks(2656)
                        },
                        new
                        {
                            Id = "CVX",
                            CurrentPrice = 166.27m,
                            Index = 18,
                            Industry = "Oil and gas",
                            Name = "Chevron",
                            PriceUpdatedOn = new DateTime(2022, 3, 29, 6, 47, 32, 63, DateTimeKind.Utc).AddTicks(2657)
                        },
                        new
                        {
                            Id = "MA",
                            CurrentPrice = 328.13m,
                            Index = 19,
                            Industry = "Payments",
                            Name = "Mastercard",
                            PriceUpdatedOn = new DateTime(2022, 3, 29, 6, 47, 32, 63, DateTimeKind.Utc).AddTicks(2657)
                        },
                        new
                        {
                            Id = "PFE",
                            CurrentPrice = 48.75m,
                            Index = 20,
                            Industry = "Pharmaceutical and biotechnology",
                            Name = "Pfizer",
                            PriceUpdatedOn = new DateTime(2022, 3, 29, 6, 47, 32, 63, DateTimeKind.Utc).AddTicks(2658)
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("SimvestFun.ApplicationCore.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("SimvestFun.ApplicationCore.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SimvestFun.ApplicationCore.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("SimvestFun.ApplicationCore.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
