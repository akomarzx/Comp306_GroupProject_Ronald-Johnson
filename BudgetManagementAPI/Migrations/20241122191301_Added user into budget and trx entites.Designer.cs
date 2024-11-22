﻿// <auto-generated />
using System;
using BudgetManagementAPI.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BudgetManagementAPI.Migrations
{
    [DbContext(typeof(BudgetDBContext))]
    [Migration("20241122191301_Added user into budget and trx entites")]
    partial class Addeduserintobudgetandtrxentites
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BudgetManagementAPI.Database.Entity.Budget", b =>
                {
                    b.Property<int>("BudgetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BudgetId"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("BudgetName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("BudgetTypeCategoryId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("OwnerUserId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("BudgetId");

                    b.HasIndex("BudgetTypeCategoryId");

                    b.HasIndex("OwnerUserId");

                    b.ToTable("Budgets");
                });

            modelBuilder.Entity("BudgetManagementAPI.Database.Entity.Category", b =>
                {
                    b.Property<long>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("CategoryId"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryId");

                    b.HasIndex("CategoryName")
                        .IsUnique();

                    b.ToTable("Category");
                });

            modelBuilder.Entity("BudgetManagementAPI.Database.Entity.Transaction", b =>
                {
                    b.Property<int>("BudgetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BudgetId"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<long>("OwnerUserId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("TransactionTypeCategoryId")
                        .HasColumnType("bigint");

                    b.HasKey("BudgetId");

                    b.HasIndex("OwnerUserId");

                    b.HasIndex("TransactionTypeCategoryId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("BudgetManagementAPI.Database.Entity.User", b =>
                {
                    b.Property<long>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("UserId"));

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId");

                    b.HasIndex("Username")
                        .IsUnique()
                        .HasFilter("[Username] IS NOT NULL");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BudgetManagementAPI.Database.Entity.Budget", b =>
                {
                    b.HasOne("BudgetManagementAPI.Database.Entity.Category", "BudgetType")
                        .WithMany()
                        .HasForeignKey("BudgetTypeCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BudgetManagementAPI.Database.Entity.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BudgetType");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("BudgetManagementAPI.Database.Entity.Transaction", b =>
                {
                    b.HasOne("BudgetManagementAPI.Database.Entity.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BudgetManagementAPI.Database.Entity.Category", "TransactionType")
                        .WithMany()
                        .HasForeignKey("TransactionTypeCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");

                    b.Navigation("TransactionType");
                });
#pragma warning restore 612, 618
        }
    }
}
