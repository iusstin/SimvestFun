﻿// <auto-generated />
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
    [Migration("20220314083649_PriceTypeChange")]
    partial class PriceTypeChange
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

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

                    b.HasKey("Id");

                    b.ToTable("Stocks");

                    b.HasData(
                        new
                        {
                            Id = "AAPL",
                            CurrentPrice = 162.95m,
                            Index = 1,
                            Industry = "Consumer electronics, software and online services",
                            Name = "Apple"
                        },
                        new
                        {
                            Id = "MSFT",
                            CurrentPrice = 288.50m,
                            Index = 2,
                            Industry = "Multinational technology",
                            Name = "Microsoft"
                        },
                        new
                        {
                            Id = "GOOG",
                            CurrentPrice = 2677m,
                            Index = 3,
                            Industry = "Conglomerate",
                            Name = "Alphabet"
                        },
                        new
                        {
                            Id = "AMZN",
                            CurrentPrice = 2786m,
                            Index = 4,
                            Industry = "E-commerce",
                            Name = "Amazon"
                        },
                        new
                        {
                            Id = "TSLA",
                            CurrentPrice = 858.97m,
                            Index = 5,
                            Industry = "Electric vehicle and clean energy",
                            Name = "Tesla"
                        },
                        new
                        {
                            Id = "BRK.B",
                            CurrentPrice = 488245m,
                            Index = 6,
                            Industry = "Financial services",
                            Name = "Berkshire Hathaway"
                        },
                        new
                        {
                            Id = "NVDA",
                            CurrentPrice = 230.14m,
                            Index = 7,
                            Industry = "Visual computing",
                            Name = "Nvidia"
                        },
                        new
                        {
                            Id = "FB",
                            CurrentPrice = 198.50m,
                            Index = 8,
                            Industry = "Information technology",
                            Name = "Meta"
                        },
                        new
                        {
                            Id = "UNH",
                            CurrentPrice = 485.57m,
                            Index = 9,
                            Industry = "Managed healthcare insurance",
                            Name = "United Health"
                        },
                        new
                        {
                            Id = "JNJ",
                            CurrentPrice = 169.36m,
                            Index = 10,
                            Industry = "Pharmaceutical Medical devices Consumer healthcare",
                            Name = "Johnson & Johnson"
                        },
                        new
                        {
                            Id = "V",
                            CurrentPrice = 199.76m,
                            Index = 11,
                            Industry = "Financial services",
                            Name = "Visa"
                        },
                        new
                        {
                            Id = "JPM",
                            CurrentPrice = 133.44m,
                            Index = 12,
                            Industry = "Financial services",
                            Name = "JPMorgan Chase"
                        },
                        new
                        {
                            Id = "WMT",
                            CurrentPrice = 139.46m,
                            Index = 13,
                            Industry = "Retail",
                            Name = "Walmart"
                        },
                        new
                        {
                            Id = "PG",
                            CurrentPrice = 148.77m,
                            Index = 14,
                            Industry = "Consumer goods",
                            Name = "Procter & Gamble"
                        },
                        new
                        {
                            Id = "XOM",
                            CurrentPrice = 82.79m,
                            Index = 15,
                            Industry = "Oil and gas",
                            Name = "Exxon Mobil"
                        },
                        new
                        {
                            Id = "HD",
                            CurrentPrice = 317.20m,
                            Index = 16,
                            Industry = "Retail",
                            Name = "Home Depot"
                        },
                        new
                        {
                            Id = "BAC",
                            CurrentPrice = 41.04m,
                            Index = 17,
                            Industry = "Financial services",
                            Name = "Bank of America"
                        },
                        new
                        {
                            Id = "CVX",
                            CurrentPrice = 166.27m,
                            Index = 18,
                            Industry = "Oil and gas",
                            Name = "Chevron"
                        },
                        new
                        {
                            Id = "MA",
                            CurrentPrice = 328.13m,
                            Index = 19,
                            Industry = "Payments",
                            Name = "Mastercard"
                        },
                        new
                        {
                            Id = "PFE",
                            CurrentPrice = 48.75m,
                            Index = 20,
                            Industry = "Pharmaceutical and biotechnology",
                            Name = "Pfizer"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
