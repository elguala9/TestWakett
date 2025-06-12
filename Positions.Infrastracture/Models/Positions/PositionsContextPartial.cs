using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Positions.Infrastracture.Models.Positions;

public partial class PositionsContext : DbContext
{
   


    partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
    {
       

        modelBuilder.Entity<Position>(entity =>
        {
 
            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v7()")
                .HasColumnName("id").HasDefaultValueSql("uuid_generate_v7()")
              .ValueGeneratedOnAdd(); 

        });

        modelBuilder.Entity<PositionValuation>(entity =>
        {

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v7()")
                .HasColumnName("id").HasDefaultValueSql("uuid_generate_v7()")
              .ValueGeneratedOnAdd();

        });

    }

}
