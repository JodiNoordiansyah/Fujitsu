﻿// <auto-generated />
using System;
using FujitsuWebApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FujitsuWebApp.Migrations
{
    [DbContext(typeof(DbFujitsuContext))]
    [Migration("20240401042125_renametable")]
    partial class renametable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FujitsuWebApp.TbMCity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Province")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TB_M_CITY");
                });

            modelBuilder.Entity("FujitsuWebApp.TbMSupplier", b =>
                {
                    b.Property<string>("SupplierCode")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("SUPPLIER_CODE");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("ADDRESS");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("CITY");

                    b.Property<string>("Pic")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("PIC");

                    b.Property<string>("Province")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("PROVINCE");

                    b.Property<string>("SupplierName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("SUPPLIER_NAME");

                    b.HasKey("SupplierCode")
                        .HasName("PK_TB_M_SUPPLIER_SUPPLIER_CODE");

                    b.ToTable("TB_M_SUPPLIER", (string)null);
                });

            modelBuilder.Entity("FujitsuWebApp.TbROrderH", b =>
                {
                    b.Property<string>("OrderNo")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("ORDER_NO");

                    b.Property<decimal?>("Amount")
                        .HasColumnType("numeric(18, 0)")
                        .HasColumnName("AMOUNT");

                    b.Property<DateOnly?>("OrderDate")
                        .HasColumnType("date")
                        .HasColumnName("ORDER_DATE");

                    b.Property<string>("SupplierCode")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("SUPPLIER_CODE");

                    b.HasKey("OrderNo")
                        .HasName("PK_TB_R_ORDER_H_ORDER_NO");

                    b.ToTable("TB_R_ORDER_H", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}