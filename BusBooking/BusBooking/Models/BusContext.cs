using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BusBooking.Models
{
    public partial class BusContext : DbContext
    {
        public virtual DbSet<Buses> Buses { get; set; }
        public virtual DbSet<Price> Price { get; set; }
        public virtual DbSet<Type> Type { get; set; }

        public BusContext(DbContextOptions<BusContext> options) : base(options)
        { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Buses>(entity =>
            {
                entity.HasKey(e => e.BusId);

                entity.Property(e => e.Name).HasMaxLength(30);
            });

            modelBuilder.Entity<Price>(entity =>
            {
                entity.Property(e => e.Cost).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.TypeId)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.HasOne(d => d.Bus)
                    .WithMany(p => p.Price)
                    .HasForeignKey(d => d.BusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BusPrice");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Price)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TypePrice");
            });

            modelBuilder.Entity<Type>(entity =>
            {
                entity.Property(e => e.TypeId)
                    .HasMaxLength(20)
                    .ValueGeneratedNever();

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.TypeName)
                    .IsRequired()
                    .HasMaxLength(30);
            });
        }
    }
}
