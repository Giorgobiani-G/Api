﻿// <auto-generated />
using System;
using LearnApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LearnApi.Migrations
{
    [DbContext(typeof(CitizenDbContext))]
    partial class CitizenDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LearnApi.Models.ConnectedPerson", b =>
                {
                    b.Property<int>("ConnectedPersonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FizikPiriId")
                        .HasColumnType("int");

                    b.Property<int>("PersonTobeConnecedId")
                        .HasColumnType("int");

                    b.Property<string>("RelationType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ConnectedPersonId");

                    b.HasIndex("FizikPiriId");

                    b.ToTable("ConnectedPersons");
                });

            modelBuilder.Entity("LearnApi.Models.ContactInfo", b =>
                {
                    b.Property<int>("ContactId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FizikPiriId")
                        .HasColumnType("int");

                    b.Property<int>("Info")
                        .HasColumnType("int");

                    b.HasKey("ContactId");

                    b.HasIndex("FizikPiriId");

                    b.ToTable("ContactInfo");
                });

            modelBuilder.Entity("LearnApi.Models.FizikPiri", b =>
                {
                    b.Property<int>("FizikPiriId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DabTarigi")
                        .HasColumnType("datetime2");

                    b.Property<string>("Gvari")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GvariLatinuri")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Misamarti")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Piradoba")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Saxeli")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SaxeliLatinuri")
                        .HasColumnType("varchar(50)");

                    b.HasKey("FizikPiriId");

                    b.ToTable("FizikPiris");
                });

            modelBuilder.Entity("LearnApi.Models.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FizikPiriId")
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FizikPiriId")
                        .IsUnique();

                    b.ToTable("Images");
                });

            modelBuilder.Entity("LearnApi.Models.ConnectedPerson", b =>
                {
                    b.HasOne("LearnApi.Models.FizikPiri", "FizikPiri")
                        .WithMany("ConnectedPersons")
                        .HasForeignKey("FizikPiriId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FizikPiri");
                });

            modelBuilder.Entity("LearnApi.Models.ContactInfo", b =>
                {
                    b.HasOne("LearnApi.Models.FizikPiri", null)
                        .WithMany("ContactInfos")
                        .HasForeignKey("FizikPiriId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LearnApi.Models.Image", b =>
                {
                    b.HasOne("LearnApi.Models.FizikPiri", "FizikPiri")
                        .WithOne("Image")
                        .HasForeignKey("LearnApi.Models.Image", "FizikPiriId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FizikPiri");
                });

            modelBuilder.Entity("LearnApi.Models.FizikPiri", b =>
                {
                    b.Navigation("ConnectedPersons");

                    b.Navigation("ContactInfos");

                    b.Navigation("Image");
                });
#pragma warning restore 612, 618
        }
    }
}
