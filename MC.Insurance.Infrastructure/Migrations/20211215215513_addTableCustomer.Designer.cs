﻿// <auto-generated />
using System;
using MC.Insurance.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MC.Insurance.Infrastructure.Migrations
{
    [DbContext(typeof(InsuranceContext))]
    [Migration("20211215215513_addTableCustomer")]
    partial class addTableCustomer
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.22")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MC.Insurance.DTO.CustomerInsurance", b =>
                {
                    b.Property<int>("customerInsuranceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("coverage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("customerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("document")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("insuranceId")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("period")
                        .HasColumnType("int");

                    b.Property<float>("price")
                        .HasColumnType("real");

                    b.Property<int>("risk")
                        .HasColumnType("int");

                    b.Property<DateTime>("start")
                        .HasColumnType("datetime2");

                    b.Property<int>("type")
                        .HasColumnType("int");

                    b.HasKey("customerInsuranceId");

                    b.ToTable("CustomerInsurances");
                });

            modelBuilder.Entity("MC.Insurance.DTO.Insurance", b =>
                {
                    b.Property<int>("insuranceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("coverage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("period")
                        .HasColumnType("int");

                    b.Property<float>("price")
                        .HasColumnType("real");

                    b.Property<int>("risk")
                        .HasColumnType("int");

                    b.Property<DateTime>("start")
                        .HasColumnType("datetime2");

                    b.Property<int>("type")
                        .HasColumnType("int");

                    b.HasKey("insuranceId");

                    b.ToTable("Insurances");
                });

            modelBuilder.Entity("MC.Insurance.DTO.tbCustomer", b =>
                {
                    b.Property<int>("customerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("customerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("document")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("customerId");

                    b.ToTable("Customer");
                });
#pragma warning restore 612, 618
        }
    }
}
