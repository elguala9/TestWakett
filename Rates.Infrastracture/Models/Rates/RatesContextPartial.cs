using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Rates.Infrastracture.Models.Rates;

public partial class RatesContext : DbContext
{
   


    partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
    {


        modelBuilder.Entity<Rate>(entity =>
        {

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id")
                .HasDefaultValueSql("uuid_generate_v7()")
              .ValueGeneratedOnAdd(); ;

        });

        modelBuilder.Entity<RateChangeNotification>(entity =>
        {


            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id").HasDefaultValueSql("uuid_generate_v7()")
              .ValueGeneratedOnAdd(); ;

        });


    }

}
