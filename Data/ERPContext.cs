using System;
using System.Collections.Generic;
using ERP.EFModels;
using Microsoft.EntityFrameworkCore;

namespace ERP.Data;

public partial class ERPContext : DbContext
{
    public ERPContext()
    {
    }

    public ERPContext(DbContextOptions<ERPContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserDetail> UserDetails { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    {

		IConfigurationRoot configuration = new ConfigurationBuilder()
				 .SetBasePath(Directory.GetCurrentDirectory())
				 .AddJsonFile("appsettings.json")
				 .Build();
		var connectionString = configuration.GetConnectionString("DefaultConnection");

		optionsBuilder.UseSqlServer(connectionString);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ROLES__3214EC07D10A0F0A");

            entity.ToTable("ROLES");

            entity.HasIndex(e => e.Name, "UQ__ROLES__737584F6BB0C794E").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("Created_at");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__USERS__3214EC07C54355F1");

            entity.ToTable("USERS");

            entity.HasIndex(e => e.Email, "uq_users_email").IsUnique();

            entity.HasIndex(e => e.Phone, "uq_users_phone").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("Created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("Updated_at");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_role");
        });

        modelBuilder.Entity<UserDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__USER_DET__3214EC0760C76CD7");

            entity.ToTable("USER_DETAILS");

            entity.Property(e => e.AddressLine1)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Address_Line_1");
            entity.Property(e => e.AddressLine2)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Address_Line_2");
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Country)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("Created_at");
            entity.Property(e => e.PostalCode)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("Postal_code");
            entity.Property(e => e.State)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("Updated_at");
            entity.Property(e => e.UserId).HasColumnName("User_Id");

            entity.HasOne(d => d.User).WithMany(p => p.UserDetails)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_userdetail");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
