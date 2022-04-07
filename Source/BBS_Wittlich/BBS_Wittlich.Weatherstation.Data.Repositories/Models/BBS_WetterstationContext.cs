using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BBS_Wittlich.Weatherstation.Data.Models
{
    public partial class BBS_WetterstationContext : DbContext
    {
        public BBS_WetterstationContext()
        {
        }

        public BBS_WetterstationContext(DbContextOptions<BBS_WetterstationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Mqtt> Mqtts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("server=192.168.0.10;userid=WebServer;password=Wittlich;database=BBS_Wetterstation");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Mqtt>(entity =>
            {
                entity.ToTable("MQTT");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Topic)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
