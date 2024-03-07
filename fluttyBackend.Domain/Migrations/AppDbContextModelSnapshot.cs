﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using fluttyBackend.Domain.Data;

#nullable disable

namespace fluttyBackend.Domain.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.26")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("fluttyBackend.Domain.Models.CartItem.CartItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Count")
                        .HasColumnType("integer");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<int>("UtcAddedTime")
                        .HasColumnType("integer");

                    b.Property<int>("UtcUpdatedTime")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("tbl_cart_item");
                });

            modelBuilder.Entity("fluttyBackend.Domain.Models.Company.CompanyTbl", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AboutCompany")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<bool>("Approved")
                        .HasColumnType("boolean");

                    b.Property<bool>("Blocked")
                        .HasColumnType("boolean");

                    b.Property<Guid>("FounderId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<string>("Photo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("FounderId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("tbl_company");
                });

            modelBuilder.Entity("fluttyBackend.Domain.Models.Company.OnRequest.CompanyAdditionRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<Guid>("OwnerUserId")
                        .HasColumnType("uuid");

                    b.Property<string>("Photo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("OwnerUserId");

                    b.ToTable("tbl_company_addition_request");
                });

            modelBuilder.Entity("fluttyBackend.Domain.Models.Company.OtMCompanyEmployees", b =>
                {
                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uuid");

                    b.HasKey("EmployeeId");

                    b.HasIndex("CompanyId");

                    b.ToTable("tbl_otm_company_employees");
                });

            modelBuilder.Entity("fluttyBackend.Domain.Models.DeliveryProduct.DeliveryProduct", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(190)
                        .HasColumnType("character varying(190)");

                    b.Property<string>("Coordinate")
                        .IsRequired()
                        .HasMaxLength(190)
                        .HasColumnType("character varying(190)");

                    b.Property<bool>("Finished")
                        .HasColumnType("boolean");

                    b.Property<string>("FinishedCause")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<bool>("FullDelivered")
                        .HasColumnType("boolean");

                    b.Property<bool>("FullPrice")
                        .HasColumnType("boolean");

                    b.Property<int>("OrderId")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<int>("UtcAddedTime")
                        .HasColumnType("integer");

                    b.Property<int>("UtcUpdatedTime")
                        .HasColumnType("integer");

                    b.Property<int>("UtcWillFinishedTime")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("tbl_delivery_product");
                });

            modelBuilder.Entity("fluttyBackend.Domain.Models.DeliveryProduct.OtMDeliveryProduct", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<double>("ByPrice")
                        .HasColumnType("double precision");

                    b.Property<bool>("Delivered")
                        .HasColumnType("boolean");

                    b.Property<Guid>("DeliveryOrderId")
                        .HasColumnType("uuid");

                    b.Property<bool>("Finished")
                        .HasColumnType("boolean");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<int>("UTCFinishedAt")
                        .HasColumnType("integer");

                    b.Property<int>("UtcAddedTime")
                        .HasColumnType("integer");

                    b.Property<int>("UtcUpdatedTime")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DeliveryOrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("tbl_otm_delivery_product");
                });

            modelBuilder.Entity("fluttyBackend.Domain.Models.ProductEntities.OnRequest.ProductAdditionRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<List<string>>("AdditionalPhotos")
                        .HasColumnType("text[]");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Photo")
                        .IsRequired()
                        .HasMaxLength(90)
                        .HasColumnType("character varying(90)");

                    b.Property<double>("Price")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("tbl_product_addition_request");
                });

            modelBuilder.Entity("fluttyBackend.Domain.Models.ProductEntities.OtMPhotosOfProduct", b =>
                {
                    b.Property<Guid>("PhotoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.HasKey("PhotoId");

                    b.HasIndex("FileName")
                        .IsUnique();

                    b.HasIndex("ProductId");

                    b.ToTable("tbl_otm_photo_of_product");
                });

            modelBuilder.Entity("fluttyBackend.Domain.Models.ProductEntities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("Blocked")
                        .HasColumnType("boolean");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<bool>("InProduction")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Photo")
                        .IsRequired()
                        .HasMaxLength(90)
                        .HasColumnType("character varying(90)");

                    b.Property<double>("Price")
                        .HasColumnType("double precision");

                    b.Property<double>("Rating")
                        .HasColumnType("double precision");

                    b.Property<bool>("Verified")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("tbl_product");
                });

            modelBuilder.Entity("fluttyBackend.Domain.Models.UserRoleEntities.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(325)
                        .HasColumnType("character varying(325)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("character varying(120)");

                    b.Property<string>("LastName")
                        .HasMaxLength(120)
                        .HasColumnType("character varying(120)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("tbl_user");
                });

            modelBuilder.Entity("fluttyBackend.Domain.Models.CartItem.CartItem", b =>
                {
                    b.HasOne("fluttyBackend.Domain.Models.ProductEntities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("fluttyBackend.Domain.Models.UserRoleEntities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("fluttyBackend.Domain.Models.Company.CompanyTbl", b =>
                {
                    b.HasOne("fluttyBackend.Domain.Models.UserRoleEntities.User", "Founder")
                        .WithMany()
                        .HasForeignKey("FounderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Founder");
                });

            modelBuilder.Entity("fluttyBackend.Domain.Models.Company.OnRequest.CompanyAdditionRequest", b =>
                {
                    b.HasOne("fluttyBackend.Domain.Models.UserRoleEntities.User", "Founder")
                        .WithMany()
                        .HasForeignKey("OwnerUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Founder");
                });

            modelBuilder.Entity("fluttyBackend.Domain.Models.Company.OtMCompanyEmployees", b =>
                {
                    b.HasOne("fluttyBackend.Domain.Models.Company.CompanyTbl", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("fluttyBackend.Domain.Models.UserRoleEntities.User", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("fluttyBackend.Domain.Models.DeliveryProduct.DeliveryProduct", b =>
                {
                    b.HasOne("fluttyBackend.Domain.Models.UserRoleEntities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("fluttyBackend.Domain.Models.DeliveryProduct.OtMDeliveryProduct", b =>
                {
                    b.HasOne("fluttyBackend.Domain.Models.DeliveryProduct.DeliveryProduct", "DeliveryProduct")
                        .WithMany()
                        .HasForeignKey("DeliveryOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("fluttyBackend.Domain.Models.ProductEntities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DeliveryProduct");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("fluttyBackend.Domain.Models.ProductEntities.OtMPhotosOfProduct", b =>
                {
                    b.HasOne("fluttyBackend.Domain.Models.ProductEntities.Product", "Product")
                        .WithMany("AdditionalPhotos")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("fluttyBackend.Domain.Models.ProductEntities.Product", b =>
                {
                    b.HasOne("fluttyBackend.Domain.Models.Company.CompanyTbl", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("fluttyBackend.Domain.Models.ProductEntities.Product", b =>
                {
                    b.Navigation("AdditionalPhotos");
                });
#pragma warning restore 612, 618
        }
    }
}
