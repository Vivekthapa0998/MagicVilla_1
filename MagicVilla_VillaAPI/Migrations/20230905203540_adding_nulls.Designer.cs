﻿// <auto-generated />
using System;
using MagicVilla_VillaAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MagicVilla_VillaAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230905203540_adding_nulls")]
    partial class adding_nulls
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MagicVilla_VillaAPI.Models.Villa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Amenity")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Details")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Occupancy")
                        .HasColumnType("int");

                    b.Property<double>("Rate")
                        .HasColumnType("float");

                    b.Property<int>("Sqft")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Villas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amenity = 1,
                            CreatedDate = new DateTime(2023, 9, 6, 2, 5, 40, 177, DateTimeKind.Local).AddTicks(6310),
                            Details = "this villa is very beautiful villa with beautiful surrounding and sunrise view",
                            ImageUrl = "https://stock.adobe.com/in/images/tropical-villa-at-night/309464116",
                            Name = "Royal villa",
                            Occupancy = 4,
                            Rate = 20000.0,
                            Sqft = 2100,
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            Amenity = 1,
                            CreatedDate = new DateTime(2023, 9, 6, 2, 5, 40, 177, DateTimeKind.Local).AddTicks(6323),
                            Details = "this villa is very beautiful villa with beautiful surrounding and sunrise view",
                            ImageUrl = "https://stock.adobe.com/in/images/tropical-villa-at-night/309464116",
                            Name = "vivek villa",
                            Occupancy = 4,
                            Rate = 20000.0,
                            Sqft = 2100,
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 3,
                            Amenity = 1,
                            CreatedDate = new DateTime(2023, 9, 6, 2, 5, 40, 177, DateTimeKind.Local).AddTicks(6325),
                            Details = "this villa is very beautiful villa with beautiful surrounding and sunrise view",
                            ImageUrl = "https://stock.adobe.com/in/images/tropical-villa-at-night/309464116",
                            Name = "Royal hill villa",
                            Occupancy = 4,
                            Rate = 20000.0,
                            Sqft = 2100,
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 4,
                            Amenity = 1,
                            CreatedDate = new DateTime(2023, 9, 6, 2, 5, 40, 177, DateTimeKind.Local).AddTicks(6326),
                            Details = "this villa is very beautiful villa with beautiful surrounding and sunrise view",
                            ImageUrl = "https://stock.adobe.com/in/images/tropical-villa-at-night/309464116",
                            Name = "villa in hills",
                            Occupancy = 4,
                            Rate = 20000.0,
                            Sqft = 2100,
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 5,
                            Amenity = 1,
                            CreatedDate = new DateTime(2023, 9, 6, 2, 5, 40, 177, DateTimeKind.Local).AddTicks(6328),
                            Details = "this villa is very beautiful villa with beautiful surrounding and sunrise view",
                            ImageUrl = "https://stock.adobe.com/in/images/tropical-villa-at-night/309464116",
                            Name = "villa rosemary",
                            Occupancy = 4,
                            Rate = 20000.0,
                            Sqft = 2100,
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("MagicVilla_VillaAPI.Models.VillaNumber", b =>
                {
                    b.Property<int>("VillaNo")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SpecialDetails")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("VillaId")
                        .HasColumnType("int");

                    b.HasKey("VillaNo");

                    b.HasIndex("VillaId");

                    b.ToTable("VillaNumbers");
                });

            modelBuilder.Entity("MagicVilla_VillaAPI.Models.VillaNumber", b =>
                {
                    b.HasOne("MagicVilla_VillaAPI.Models.Villa", "Villa")
                        .WithMany()
                        .HasForeignKey("VillaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Villa");
                });
#pragma warning restore 612, 618
        }
    }
}
