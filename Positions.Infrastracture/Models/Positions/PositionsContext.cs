using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Positions.Infrastracture.Models.Positions;

public partial class PositionsContext : DbContext
{
    public PositionsContext(DbContextOptions<PositionsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<PositionValuation> PositionValuations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");

        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("positions_pkey");

            entity.ToTable("positions");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v7()")
                .HasColumnName("id");
            entity.Property(e => e.Initialrate)
                .HasPrecision(18, 8)
                .HasColumnName("initialrate");
            entity.Property(e => e.Instrumentid)
                .HasMaxLength(20)
                .HasColumnName("instrumentid");
            entity.Property(e => e.Isclosed)
                .HasDefaultValue(false)
                .HasColumnName("isclosed");
            entity.Property(e => e.Openedat).HasColumnName("openedat");
            entity.Property(e => e.Quantity)
                .HasPrecision(18, 8)
                .HasColumnName("quantity");
            entity.Property(e => e.Side).HasColumnName("side");
        });

        modelBuilder.Entity<PositionValuation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("position_valuations_pkey");

            entity.ToTable("position_valuations");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v7()")
                .HasColumnName("id");
            entity.Property(e => e.Calculatedat).HasColumnName("calculatedat");
            entity.Property(e => e.Currentrate)
                .HasPrecision(18, 8)
                .HasColumnName("currentrate");
            entity.Property(e => e.Positionid).HasColumnName("positionid");
            entity.Property(e => e.Profitloss)
                .HasPrecision(18, 8)
                .HasColumnName("profitloss");

            entity.HasOne(d => d.Position).WithMany(p => p.PositionValuations)
                .HasForeignKey(d => d.Positionid)
                .HasConstraintName("position_valuations_positionid_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
