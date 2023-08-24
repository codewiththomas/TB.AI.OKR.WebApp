﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TB.AI.OKR.WebApp.Persistence.Contexts;

#nullable disable

namespace TB.AI.OKR.WebApp.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.10");

            modelBuilder.Entity("TB.AI.OKR.WebApp.Persistence.Entities.KeyResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ObjectiveId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ObjectiveId");

                    b.ToTable("KeyResults");
                });

            modelBuilder.Entity("TB.AI.OKR.WebApp.Persistence.Entities.Objective", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Objectives");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Objective");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("TB.AI.OKR.WebApp.Persistence.Entities.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Provider")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Result")
                        .HasColumnType("TEXT");

                    b.Property<TimeSpan>("ReviewTime")
                        .HasColumnType("TEXT");

                    b.Property<int?>("SampleOkrId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("SampleOkrId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("TB.AI.OKR.WebApp.Persistence.Entities.SampleOkr", b =>
                {
                    b.HasBaseType("TB.AI.OKR.WebApp.Persistence.Entities.Objective");

                    b.Property<string>("Source")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasDiscriminator().HasValue("SampleOkr");
                });

            modelBuilder.Entity("TB.AI.OKR.WebApp.Persistence.Entities.KeyResult", b =>
                {
                    b.HasOne("TB.AI.OKR.WebApp.Persistence.Entities.Objective", null)
                        .WithMany("KeyResults")
                        .HasForeignKey("ObjectiveId");
                });

            modelBuilder.Entity("TB.AI.OKR.WebApp.Persistence.Entities.Review", b =>
                {
                    b.HasOne("TB.AI.OKR.WebApp.Persistence.Entities.SampleOkr", null)
                        .WithMany("Reviews")
                        .HasForeignKey("SampleOkrId");
                });

            modelBuilder.Entity("TB.AI.OKR.WebApp.Persistence.Entities.Objective", b =>
                {
                    b.Navigation("KeyResults");
                });

            modelBuilder.Entity("TB.AI.OKR.WebApp.Persistence.Entities.SampleOkr", b =>
                {
                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}
