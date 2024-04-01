using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FujitsuWebApp.Models;

public partial class DbFujitsuContext : DbContext
{
    public DbFujitsuContext()
    {
    }

    public DbFujitsuContext(DbContextOptions<DbFujitsuContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TbMSupplier> TbMSuppliers { get; set; }

    public virtual DbSet<TbROrderH> TbROrderHs { get; set; }
    public virtual DbSet<TbMCity> TbMCity { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-6JEF5O9\\SQLEXPRESS; Database=dbFujitsu; User Id=sa; Password=123456; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TbMSupplier>(entity =>
        {
            entity.HasKey(e => e.SupplierCode).HasName("PK_TB_M_SUPPLIER_SUPPLIER_CODE");

            entity.ToTable("TB_M_SUPPLIER");

            entity.Property(e => e.SupplierCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("SUPPLIER_CODE");
            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ADDRESS");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CITY");
            entity.Property(e => e.Pic)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("PIC");
            entity.Property(e => e.Province)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PROVINCE");
            entity.Property(e => e.SupplierName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SUPPLIER_NAME");
        });

        modelBuilder.Entity<TbROrderH>(entity =>
        {
            entity.HasKey(e => e.OrderNo).HasName("PK_TB_R_ORDER_H_ORDER_NO");

            entity.ToTable("TB_R_ORDER_H");

            entity.Property(e => e.OrderNo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ORDER_NO");
            entity.Property(e => e.Amount)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("AMOUNT");
            entity.Property(e => e.OrderDate).HasColumnName("ORDER_DATE");
            entity.Property(e => e.SupplierCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("SUPPLIER_CODE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
