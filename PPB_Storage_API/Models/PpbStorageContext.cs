using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PPB_Storage_API.Models;

public partial class PpbStorageContext : DbContext
{
    public PpbStorageContext()
    {
    }

    public PpbStorageContext(DbContextOptions<PpbStorageContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Command> Commands { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<PermissionsRole> PermissionsRoles { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductsCommand> ProductsCommands { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\sqlexpress; Trusted_Connection=True; Encrypt=false; Database=PPB_Storage");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Command>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__commands__3213E83FF5FD09D7");

            entity.ToTable("commands");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Date)
                .HasColumnType("date")
                .HasColumnName("date");
            entity.Property(e => e.Delivered).HasColumnName("delivered");
            entity.Property(e => e.Number).HasColumnName("number");
            entity.Property(e => e.Ready).HasColumnName("ready");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__permissi__3213E83F2038F218");

            entity.ToTable("permissions");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<PermissionsRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__permissi__3213E83F23AB6890");

            entity.ToTable("permissions_roles");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PermissionId).HasColumnName("permissionId");
            entity.Property(e => e.RoleId).HasColumnName("roleId");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__products__3213E83FC8CC925F");

            entity.ToTable("products");

            entity.HasIndex(e => e.Barcode, "UQ__products__C16E36F88A696C3B").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Barcode)
                .HasMaxLength(13)
                .HasColumnName("barcode");
            entity.Property(e => e.Image)
                .HasMaxLength(255)
                .HasColumnName("image");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Price).HasColumnName("price");
        });

        modelBuilder.Entity<ProductsCommand>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__products__3213E83F76290C1C");

            entity.ToTable("products_commands");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CommandId).HasColumnName("commandId");
            entity.Property(e => e.ProductId).HasColumnName("productId");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__roles__3213E83F14E4EAC8");

            entity.ToTable("roles");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__users__3213E83F9866BA16");

            entity.ToTable("users");

            entity.HasIndex(e => e.Username, "UQ__users__F3DBC572B8EF2BA5").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .HasColumnName("last_name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
