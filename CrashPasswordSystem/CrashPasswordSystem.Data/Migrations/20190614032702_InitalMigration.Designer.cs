﻿// <auto-generated />
using System;
using CrashPasswordSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CrashPasswordSystem.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20190614032702_InitalMigration")]
    partial class InitalMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CrashPasswordSystem.Models.CrashCompany", b =>
                {
                    b.Property<int>("CCID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CCName")
                        .HasMaxLength(50);

                    b.HasKey("CCID");

                    b.ToTable("CrashCompany");
                });

            modelBuilder.Entity("CrashPasswordSystem.Models.Product", b =>
                {
                    b.Property<int>("ProductID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CCID");

                    b.Property<int>("PCID");

                    b.Property<DateTime>("ProductDateAdded");

                    b.Property<string>("ProductDescription")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<DateTime?>("ProductExpiry");

                    b.Property<string>("ProductPassword")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("ProductURL")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("ProductUsername")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.Property<int>("StaffID");

                    b.Property<int>("SupplierID");

                    b.HasKey("ProductID");

                    b.HasIndex("CCID");

                    b.HasIndex("PCID");

                    b.HasIndex("StaffID");

                    b.HasIndex("SupplierID");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("CrashPasswordSystem.Models.ProductCategory", b =>
                {
                    b.Property<int>("PCID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PCName")
                        .HasMaxLength(100);

                    b.HasKey("PCID");

                    b.ToTable("ProductCategory");
                });

            modelBuilder.Entity("CrashPasswordSystem.Models.Supplier", b =>
                {
                    b.Property<int>("SupplierID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("SupplierAddress")
                        .HasMaxLength(200);

                    b.Property<string>("SupplierContactNumber")
                        .HasMaxLength(30);

                    b.Property<DateTime>("SupplierDateAdded");

                    b.Property<string>("SupplierEmail")
                        .HasMaxLength(100);

                    b.Property<string>("SupplierName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("SupplierWebsite")
                        .HasMaxLength(100);

                    b.HasKey("SupplierID");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("CrashPasswordSystem.Models.UpdateHistory", b =>
                {
                    b.Property<int>("UHID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateUpdated");

                    b.Property<int>("ProductID");

                    b.Property<int>("StaffID");

                    b.HasKey("UHID");

                    b.HasIndex("ProductID");

                    b.ToTable("UpdateHistories");
                });

            modelBuilder.Entity("CrashPasswordSystem.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool?>("UserActive");

                    b.Property<DateTime?>("UserDateCreated");

                    b.Property<string>("UserEmail")
                        .HasMaxLength(100);

                    b.Property<string>("UserFirstName")
                        .HasMaxLength(50);

                    b.Property<string>("UserHash")
                        .HasMaxLength(100);

                    b.Property<string>("UserInitials");

                    b.Property<string>("UserLastName")
                        .HasMaxLength(50);

                    b.Property<string>("UserSalt")
                        .HasMaxLength(20);

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CrashPasswordSystem.Models.Product", b =>
                {
                    b.HasOne("CrashPasswordSystem.Models.CrashCompany", "Company")
                        .WithMany("Products")
                        .HasForeignKey("CCID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CrashPasswordSystem.Models.ProductCategory", "ProductCategory")
                        .WithMany("Products")
                        .HasForeignKey("PCID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CrashPasswordSystem.Models.User", "Staff")
                        .WithMany("Products")
                        .HasForeignKey("StaffID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CrashPasswordSystem.Models.Supplier", "Supplier")
                        .WithMany("Products")
                        .HasForeignKey("SupplierID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CrashPasswordSystem.Models.UpdateHistory", b =>
                {
                    b.HasOne("CrashPasswordSystem.Models.Product", "Product")
                        .WithMany("UpdateHistories")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
