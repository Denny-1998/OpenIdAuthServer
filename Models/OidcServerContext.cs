using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace OpenIdAuthServer.Models;

public partial class OidcServerContext : DbContext
{
    public OidcServerContext()
    {
    }

    public OidcServerContext(DbContextOptions<OidcServerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Token> Tokens { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=oidcServer;User Id=sa;Password=Baum123456;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.ClientId).HasName("PK__Clients__E67E1A248C48DE35");

            entity.Property(e => e.ClientId).HasMaxLength(256);
            entity.Property(e => e.ClientName).HasMaxLength(256);
            entity.Property(e => e.ClientSecret).HasMaxLength(256);
            entity.Property(e => e.RedirectUri).HasMaxLength(256);
        });

        modelBuilder.Entity<Token>(entity =>
        {
            entity.HasKey(e => e.Token1).HasName("PK__Tokens__1EB4F8162C32E93A");

            entity.Property(e => e.Token1)
                .HasMaxLength(256)
                .HasColumnName("Token");
            entity.Property(e => e.ClientId).HasMaxLength(256);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ExpiresAt).HasColumnType("datetime");
            entity.Property(e => e.TokenType).HasMaxLength(50);

            entity.HasOne(d => d.Client).WithMany(p => p.Tokens)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tokens__ClientId__29572725");

            entity.HasOne(d => d.User).WithMany(p => p.Tokens)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tokens__UserId__286302EC");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("email");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(128)
                .IsFixedLength()
                .HasColumnName("passwordHash");
            entity.Property(e => e.Salt)
                .HasMaxLength(128)
                .IsFixedLength()
                .HasColumnName("salt");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
