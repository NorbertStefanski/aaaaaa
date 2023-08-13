﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OrderManagement.Infrastructure;

#nullable disable

namespace OrderManagement.Infrastructure.Migrations
{
    [DbContext(typeof(OrderManagementContext))]
    [Migration("20230109113856_AddImageUrl")]
    partial class AddImageUrl
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("OrderManagement.Domain.Bar", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Bars");
                });

            modelBuilder.Entity("OrderManagement.Domain.Cocktail", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Cocktails");
                });

            modelBuilder.Entity("OrderManagement.Domain.MenuItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("BarId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CocktailId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("BarId");

                    b.HasIndex("CocktailId");

                    b.ToTable("MenuItems");
                });

            modelBuilder.Entity("OrderManagement.Domain.MenuItem", b =>
                {
                    b.HasOne("OrderManagement.Domain.Bar", null)
                        .WithMany("MenuItems")
                        .HasForeignKey("BarId");

                    b.HasOne("OrderManagement.Domain.Cocktail", "Cocktail")
                        .WithMany()
                        .HasForeignKey("CocktailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cocktail");
                });

            modelBuilder.Entity("OrderManagement.Domain.Bar", b =>
                {
                    b.Navigation("MenuItems");
                });
#pragma warning restore 612, 618
        }
    }
}
