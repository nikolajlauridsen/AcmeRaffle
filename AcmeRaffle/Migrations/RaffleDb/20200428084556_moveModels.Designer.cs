﻿// <auto-generated />
using System;
using AcmeRaffle.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AcmeRaffle.Migrations.RaffleDb
{
    [DbContext(typeof(RaffleDbContext))]
    [Migration("20200428084556_moveModels")]
    partial class moveModels
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3");

            modelBuilder.Entity("RaffleLogic.Models.RaffleEntry", b =>
                {
                    b.Property<int>("RaffleEntryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Age")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("SoldProductId")
                        .HasColumnType("INTEGER");

                    b.HasKey("RaffleEntryId");

                    b.HasIndex("SoldProductId");

                    b.ToTable("Entries");
                });

            modelBuilder.Entity("RaffleLogic.Models.SoldProduct", b =>
                {
                    b.Property<int>("SoldProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("SerialNumber")
                        .HasColumnType("TEXT");

                    b.HasKey("SoldProductId");

                    b.ToTable("SoldProducts");
                });

            modelBuilder.Entity("RaffleLogic.Models.RaffleEntry", b =>
                {
                    b.HasOne("RaffleLogic.Models.SoldProduct", "SoldProduct")
                        .WithMany()
                        .HasForeignKey("SoldProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
