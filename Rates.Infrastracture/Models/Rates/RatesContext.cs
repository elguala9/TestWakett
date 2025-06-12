using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Rates.Infrastracture.Models.Rates;

public partial class RatesContext : DbContext
{
    public RatesContext(DbContextOptions<RatesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Rate> Rates { get; set; }

    public virtual DbSet<RateChangeNotification> RateChangeNotifications { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");

        modelBuilder.Entity<Rate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("rates_pkey");

            entity.ToTable("rates");

            entity.HasIndex(e => new { e.Symbol, e.Timestamp }, "ix_rates_symbol_timestamp");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Symbol)
                .HasMaxLength(20)
                .HasColumnName("symbol");
            entity.Property(e => e.Timestamp).HasColumnName("timestamp");
            entity.Property(e => e.Value)
                .HasPrecision(18, 8)
                .HasColumnName("value");
        });

        modelBuilder.Entity<RateChangeNotification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("rate_change_notifications_pkey");

            entity.ToTable("rate_change_notifications");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Changepercent).HasColumnName("changepercent");
            entity.Property(e => e.Newrate)
                .HasPrecision(18, 8)
                .HasColumnName("newrate");
            entity.Property(e => e.Notifiedat).HasColumnName("notifiedat");
            entity.Property(e => e.Oldrate)
                .HasPrecision(18, 8)
                .HasColumnName("oldrate");
            entity.Property(e => e.Symbol)
                .HasMaxLength(20)
                .HasColumnName("symbol");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
