﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TB.AI.OKR.WebApp.Persistence.Contexts;

#nullable disable

namespace TB.AI.OKR.WebApp.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231003083939_ReferencesAdded")]
    partial class ReferencesAdded
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.10");

            modelBuilder.Entity("OkrRuleReferenceSource", b =>
                {
                    b.Property<int>("OkrRulesId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ReferencesId")
                        .HasColumnType("INTEGER");

                    b.HasKey("OkrRulesId", "ReferencesId");

                    b.HasIndex("ReferencesId");

                    b.ToTable("OkrRuleReferenceSource");
                });

            modelBuilder.Entity("OkrSetReferenceSource", b =>
                {
                    b.Property<int>("OkrSetsId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ReferencesId")
                        .HasColumnType("INTEGER");

                    b.HasKey("OkrSetsId", "ReferencesId");

                    b.HasIndex("ReferencesId");

                    b.ToTable("OkrSetReferenceSource");
                });

            modelBuilder.Entity("TB.AI.OKR.WebApp.Persistence.Entities.Label<TB.AI.OKR.WebApp.Persistence.Entities.OkrSet>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("EntityId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EntityId");

                    b.ToTable("OkrSetLabels");
                });

            modelBuilder.Entity("TB.AI.OKR.WebApp.Persistence.Entities.Label<TB.AI.OKR.WebApp.Persistence.Entities.OkrSetElement>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("EntityId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EntityId");

                    b.ToTable("OkrSetElementLabels");
                });

            modelBuilder.Entity("TB.AI.OKR.WebApp.Persistence.Entities.OkrRule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(4096)
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Scope")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Severity")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("OkrRules");

                    b.HasData(
                        new
                        {
                            Id = 19,
                            Description = "have excactly one objective",
                            IsActive = true,
                            Scope = 1,
                            Severity = 2,
                            Title = ""
                        },
                        new
                        {
                            Id = 20,
                            Description = "have not more than 5 key results",
                            IsActive = true,
                            Scope = 1,
                            Severity = 1,
                            Title = ""
                        },
                        new
                        {
                            Id = 21,
                            Description = "have at least 1 key result",
                            IsActive = true,
                            Scope = 1,
                            Severity = 2,
                            Title = ""
                        },
                        new
                        {
                            Id = 22,
                            Description = "have at least 3 key results",
                            IsActive = true,
                            Scope = 1,
                            Severity = 1,
                            Title = ""
                        },
                        new
                        {
                            Id = 23,
                            Description = "can be abbreviated with O",
                            IsActive = true,
                            Scope = 2,
                            Severity = 0,
                            Title = ""
                        },
                        new
                        {
                            Id = 24,
                            Description = "describes the \"What\"",
                            IsActive = true,
                            Scope = 2,
                            Severity = 0,
                            Title = ""
                        },
                        new
                        {
                            Id = 25,
                            Description = "expresses goals or intends",
                            IsActive = true,
                            Scope = 2,
                            Severity = 0,
                            Title = ""
                        },
                        new
                        {
                            Id = 26,
                            Description = "be aggressive, yet realistic",
                            IsActive = true,
                            Scope = 2,
                            Severity = 1,
                            Title = ""
                        },
                        new
                        {
                            Id = 27,
                            Description = "be tangible, objective, and unambigous",
                            IsActive = true,
                            Scope = 2,
                            Severity = 1,
                            Title = ""
                        },
                        new
                        {
                            Id = 28,
                            Description = "be obvious to a rational observer whether an objective has been achieved",
                            IsActive = true,
                            Scope = 2,
                            Severity = 1,
                            Title = ""
                        },
                        new
                        {
                            Id = 29,
                            Description = "provide clear value to the company when successful achieved",
                            IsActive = true,
                            Scope = 2,
                            Severity = 2,
                            Title = ""
                        },
                        new
                        {
                            Id = 30,
                            Description = "can be abbreviated with KR",
                            IsActive = true,
                            Scope = 3,
                            Severity = 0,
                            Title = ""
                        },
                        new
                        {
                            Id = 31,
                            Description = "describes the \"How\"",
                            IsActive = true,
                            Scope = 3,
                            Severity = 0,
                            Title = ""
                        },
                        new
                        {
                            Id = 32,
                            Description = "express measurable outcome",
                            IsActive = true,
                            Scope = 3,
                            Severity = 0,
                            Title = ""
                        },
                        new
                        {
                            Id = 33,
                            Description = "express an outcome instead an output",
                            IsActive = true,
                            Scope = 3,
                            Severity = 1,
                            Title = ""
                        },
                        new
                        {
                            Id = 34,
                            Description = "describe outcome, not activities (if words like consult, help, analyze, or participate are included, it describes activities)",
                            IsActive = true,
                            Scope = 3,
                            Severity = 1,
                            Title = ""
                        },
                        new
                        {
                            Id = 35,
                            Description = "measurable and verifiable",
                            IsActive = true,
                            Scope = 3,
                            Severity = 1,
                            Title = ""
                        },
                        new
                        {
                            Id = 36,
                            Description = "be difficult but not impossible to achieve",
                            IsActive = true,
                            Scope = 3,
                            Severity = 1,
                            Title = ""
                        });
                });

            modelBuilder.Entity("TB.AI.OKR.WebApp.Persistence.Entities.OkrSet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("OkrSets");
                });

            modelBuilder.Entity("TB.AI.OKR.WebApp.Persistence.Entities.OkrSetElement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("OkrSetId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("OkrSetId");

                    b.ToTable("OkrSetElements");
                });

            modelBuilder.Entity("TB.AI.OKR.WebApp.Persistence.Entities.ReferenceSource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ReferenceSymbol")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ReferenceText")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ReferenceSources");
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

                    b.Property<int?>("OkrSetId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Provider")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Result")
                        .HasColumnType("TEXT");

                    b.Property<TimeSpan>("ReviewTime")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("OkrSetId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("OkrRuleReferenceSource", b =>
                {
                    b.HasOne("TB.AI.OKR.WebApp.Persistence.Entities.OkrRule", null)
                        .WithMany()
                        .HasForeignKey("OkrRulesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TB.AI.OKR.WebApp.Persistence.Entities.ReferenceSource", null)
                        .WithMany()
                        .HasForeignKey("ReferencesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OkrSetReferenceSource", b =>
                {
                    b.HasOne("TB.AI.OKR.WebApp.Persistence.Entities.OkrSet", null)
                        .WithMany()
                        .HasForeignKey("OkrSetsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TB.AI.OKR.WebApp.Persistence.Entities.ReferenceSource", null)
                        .WithMany()
                        .HasForeignKey("ReferencesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TB.AI.OKR.WebApp.Persistence.Entities.Label<TB.AI.OKR.WebApp.Persistence.Entities.OkrSet>", b =>
                {
                    b.HasOne("TB.AI.OKR.WebApp.Persistence.Entities.OkrSet", "Entity")
                        .WithMany()
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Entity");
                });

            modelBuilder.Entity("TB.AI.OKR.WebApp.Persistence.Entities.Label<TB.AI.OKR.WebApp.Persistence.Entities.OkrSetElement>", b =>
                {
                    b.HasOne("TB.AI.OKR.WebApp.Persistence.Entities.OkrSetElement", "Entity")
                        .WithMany()
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Entity");
                });

            modelBuilder.Entity("TB.AI.OKR.WebApp.Persistence.Entities.OkrSetElement", b =>
                {
                    b.HasOne("TB.AI.OKR.WebApp.Persistence.Entities.OkrSet", null)
                        .WithMany("OkrSetElement")
                        .HasForeignKey("OkrSetId");
                });

            modelBuilder.Entity("TB.AI.OKR.WebApp.Persistence.Entities.Review", b =>
                {
                    b.HasOne("TB.AI.OKR.WebApp.Persistence.Entities.OkrSet", null)
                        .WithMany("Reviews")
                        .HasForeignKey("OkrSetId");
                });

            modelBuilder.Entity("TB.AI.OKR.WebApp.Persistence.Entities.OkrSet", b =>
                {
                    b.Navigation("OkrSetElement");

                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}
