﻿// <auto-generated />
using System;
using Marchandise_FCE.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Marchandise_FCE.Migrations
{
    [DbContext(typeof(AnnuaireContext))]
    partial class AnnuaireContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Marchandise_FCE.Models.Mcategorie", b =>
                {
                    b.Property<string>("codcat")
                        .HasMaxLength(4)
                        .HasColumnType("varchar(4)");

                    b.Property<string>("designcat")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("codcat");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Marchandise_FCE.Models.Menvoi", b =>
                {
                    b.Property<int>("numenvoi")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("codegare")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("varchar(3)");

                    b.Property<DateTime>("date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("idexp")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("varchar(6)");

                    b.Property<int>("nbenvoi")
                        .HasColumnType("int");

                    b.Property<decimal>("poids")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("refmarch")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("varchar(6)");

                    b.Property<string>("type_embal")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.HasKey("numenvoi");

                    b.HasIndex("codegare");

                    b.HasIndex("idexp");

                    b.HasIndex("refmarch");

                    b.ToTable("Envois");
                });

            modelBuilder.Entity("Marchandise_FCE.Models.Mexpediteur", b =>
                {
                    b.Property<string>("idexp")
                        .HasMaxLength(6)
                        .HasColumnType("varchar(6)");

                    b.Property<string>("cin")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("varchar(12)");

                    b.Property<string>("nomexp")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("tel")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.HasKey("idexp");

                    b.ToTable("Expediteurs");
                });

            modelBuilder.Entity("Marchandise_FCE.Models.Mgare", b =>
                {
                    b.Property<string>("codegare")
                        .HasMaxLength(3)
                        .HasColumnType("varchar(3)");

                    b.Property<string>("Mgarecodegare")
                        .HasColumnType("varchar(3)");

                    b.Property<string>("abrevnom")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("varchar(3)");

                    b.Property<string>("codezone")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("varchar(3)");

                    b.Property<int>("kilometrage")
                        .HasColumnType("int");

                    b.Property<string>("nomgare")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("codegare");

                    b.HasIndex("Mgarecodegare");

                    b.HasIndex("codezone");

                    b.ToTable("Gares");
                });

            modelBuilder.Entity("Marchandise_FCE.Models.Mmarchandise", b =>
                {
                    b.Property<string>("refmarch")
                        .HasMaxLength(4)
                        .HasColumnType("varchar(4)");

                    b.Property<string>("codcat")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("varchar(4)");

                    b.Property<string>("nature")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("refmarch");

                    b.HasIndex("codcat");

                    b.ToTable("Marchandises");
                });

            modelBuilder.Entity("Marchandise_FCE.Models.Mtarif", b =>
                {
                    b.Property<string>("idtarif")
                        .HasMaxLength(3)
                        .HasColumnType("varchar(3)");

                    b.Property<string>("codcat")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("varchar(4)");

                    b.Property<string>("codezone")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("varchar(3)");

                    b.Property<int>("prixkilo")
                        .HasColumnType("int");

                    b.HasKey("idtarif");

                    b.HasIndex("codcat");

                    b.HasIndex("codezone");

                    b.ToTable("Tarifs");
                });

            modelBuilder.Entity("Marchandise_FCE.Models.Mzone", b =>
                {
                    b.Property<string>("codezone")
                        .HasMaxLength(3)
                        .HasColumnType("varchar(3)");

                    b.Property<string>("descriptzone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("nomzone")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("codezone");

                    b.ToTable("Zones");
                });

            modelBuilder.Entity("Marchandise_FCE.Models.Menvoi", b =>
                {
                    b.HasOne("Marchandise_FCE.Models.Mgare", "Gare")
                        .WithMany("Envois")
                        .HasForeignKey("codegare")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Marchandise_FCE.Models.Mexpediteur", "Expediteur")
                        .WithMany("Envois")
                        .HasForeignKey("idexp")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Marchandise_FCE.Models.Mmarchandise", "Marchandise")
                        .WithMany("Envois")
                        .HasForeignKey("refmarch")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Expediteur");

                    b.Navigation("Gare");

                    b.Navigation("Marchandise");
                });

            modelBuilder.Entity("Marchandise_FCE.Models.Mgare", b =>
                {
                    b.HasOne("Marchandise_FCE.Models.Mgare", null)
                        .WithMany("Gares")
                        .HasForeignKey("Mgarecodegare");

                    b.HasOne("Marchandise_FCE.Models.Mzone", "Zones")
                        .WithMany("Gares")
                        .HasForeignKey("codezone")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Zones");
                });

            modelBuilder.Entity("Marchandise_FCE.Models.Mmarchandise", b =>
                {
                    b.HasOne("Marchandise_FCE.Models.Mcategorie", "Categorie")
                        .WithMany("Marchandises")
                        .HasForeignKey("codcat")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categorie");
                });

            modelBuilder.Entity("Marchandise_FCE.Models.Mtarif", b =>
                {
                    b.HasOne("Marchandise_FCE.Models.Mcategorie", "Categorie")
                        .WithMany("Tarifs")
                        .HasForeignKey("codcat")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Marchandise_FCE.Models.Mzone", "Zone")
                        .WithMany("Tarifs")
                        .HasForeignKey("codezone")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categorie");

                    b.Navigation("Zone");
                });

            modelBuilder.Entity("Marchandise_FCE.Models.Mcategorie", b =>
                {
                    b.Navigation("Marchandises");

                    b.Navigation("Tarifs");
                });

            modelBuilder.Entity("Marchandise_FCE.Models.Mexpediteur", b =>
                {
                    b.Navigation("Envois");
                });

            modelBuilder.Entity("Marchandise_FCE.Models.Mgare", b =>
                {
                    b.Navigation("Envois");

                    b.Navigation("Gares");
                });

            modelBuilder.Entity("Marchandise_FCE.Models.Mmarchandise", b =>
                {
                    b.Navigation("Envois");
                });

            modelBuilder.Entity("Marchandise_FCE.Models.Mzone", b =>
                {
                    b.Navigation("Gares");

                    b.Navigation("Tarifs");
                });
#pragma warning restore 612, 618
        }
    }
}