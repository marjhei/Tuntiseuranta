using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Tuntiseuranta.Models
{
    public partial class TuntiseurantaContext : DbContext
    {
        public TuntiseurantaContext()
        {
        }

        public TuntiseurantaContext(DbContextOptions<TuntiseurantaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Kayttaja> Kayttaja { get; set; }
        public virtual DbSet<Tunnit> Tunnit { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("server=localhost;database=Tuntiseuranta;trusted_connection=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Kayttaja>(entity =>
            {
                entity.Property(e => e.Etunimi)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Osasto).HasMaxLength(60);

                entity.Property(e => e.Sukunimi)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.Tehtavanimike).HasMaxLength(60);
            });

            modelBuilder.Entity<Tunnit>(entity =>
            {
                entity.HasKey(e => e.TuntiId)
                    .HasName("PK__Tunnit__8979CBCB1D0DAEC9");

                entity.Property(e => e.TuntiId).HasColumnName("Tunti_id");

                entity.Property(e => e.KayttajaId).HasColumnName("Kayttaja_id");

                entity.Property(e => e.Kuvaus)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Paivamaara).HasColumnType("datetime");

                entity.Property(e => e.Tunnit1)
                    .HasColumnName("Tunnit")
                    .HasColumnType("decimal(5, 2)");

                entity.HasOne(d => d.Kayttaja)
                    .WithMany(p => p.Tunnit)
                    .HasForeignKey(d => d.KayttajaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tunnit_Kayttaja");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
