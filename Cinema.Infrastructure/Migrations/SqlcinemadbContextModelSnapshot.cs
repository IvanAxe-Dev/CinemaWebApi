﻿// <auto-generated />
using System;
using Cinema.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Cinema.Infrastructure.Migrations
{
    [DbContext(typeof(SqlcinemadbContext))]
    partial class SqlcinemadbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Cinema.Core.Domain.Entities.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Cinema.Core.Domain.Entities.CinemaHall", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("Graphics")
                        .HasColumnType("int");

                    b.Property<int?>("Privilege")
                        .HasColumnType("int");

                    b.Property<int?>("SeatNumbers")
                        .HasColumnType("int");

                    b.Property<int?>("SeatRows")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("CinemaHalls");
                });

            modelBuilder.Entity("Cinema.Core.Domain.Entities.Movie", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Actors")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("AgeRestriction")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Director")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<TimeOnly?>("Duration")
                        .HasColumnType("time");

                    b.Property<byte[]>("Image")
                        .HasColumnType("image");

                    b.Property<string>("Ratings")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ReleaseDate")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("RentalEndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("RentalStartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("TrailerUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("Cinema.Core.Domain.Entities.MovieCategory", b =>
                {
                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MovieId")
                        .HasColumnType("uniqueidentifier");

                    b.HasIndex("CategoryId");

                    b.HasIndex("MovieId");

                    b.ToTable("MovieCategories");
                });

            modelBuilder.Entity("Cinema.Core.Domain.Entities.Seat", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CinemaHallId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsBooked")
                        .HasColumnType("bit");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<int>("Row")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CinemaHallId");

                    b.ToTable("Seats");
                });

            modelBuilder.Entity("Cinema.Core.Domain.Entities.Session", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CinemaHallId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateOnly?>("Date")
                        .HasColumnType("date");

                    b.Property<Guid?>("MovieId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<TimeOnly?>("StartTime")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.HasIndex("CinemaHallId");

                    b.HasIndex("MovieId");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("Cinema.Core.Domain.Entities.Ticket", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SeatId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SessionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SeatId");

                    b.HasIndex("SessionId");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("Cinema.Core.Domain.Entities.MovieCategory", b =>
                {
                    b.HasOne("Cinema.Core.Domain.Entities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .IsRequired()
                        .HasConstraintName("FK_MovieCategories_Categories");

                    b.HasOne("Cinema.Core.Domain.Entities.Movie", "Movie")
                        .WithMany()
                        .HasForeignKey("MovieId")
                        .IsRequired()
                        .HasConstraintName("FK_MovieCategories_Movies");

                    b.Navigation("Category");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("Cinema.Core.Domain.Entities.Seat", b =>
                {
                    b.HasOne("Cinema.Core.Domain.Entities.CinemaHall", "CinemaHall")
                        .WithMany("Seats")
                        .HasForeignKey("CinemaHallId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CinemaHall");
                });

            modelBuilder.Entity("Cinema.Core.Domain.Entities.Session", b =>
                {
                    b.HasOne("Cinema.Core.Domain.Entities.CinemaHall", null)
                        .WithMany("Sessions")
                        .HasForeignKey("CinemaHallId");

                    b.HasOne("Cinema.Core.Domain.Entities.Movie", null)
                        .WithMany("Sessions")
                        .HasForeignKey("MovieId");
                });

            modelBuilder.Entity("Cinema.Core.Domain.Entities.Ticket", b =>
                {
                    b.HasOne("Cinema.Core.Domain.Entities.Seat", "Seat")
                        .WithMany()
                        .HasForeignKey("SeatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cinema.Core.Domain.Entities.Session", "Session")
                        .WithMany()
                        .HasForeignKey("SessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Seat");

                    b.Navigation("Session");
                });

            modelBuilder.Entity("Cinema.Core.Domain.Entities.CinemaHall", b =>
                {
                    b.Navigation("Seats");

                    b.Navigation("Sessions");
                });

            modelBuilder.Entity("Cinema.Core.Domain.Entities.Movie", b =>
                {
                    b.Navigation("Sessions");
                });
#pragma warning restore 612, 618
        }
    }
}
