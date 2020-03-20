using Laborator4NetCore.Models;
using Microsoft.EntityFrameworkCore;

namespace Laborator4NetCore
{
    public class ModelContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Album> Albums { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-DDRTRS2\\NUYONUSQL;Database = EFCore2020Lab4; Trusted_Connection = True");
 }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AlbumArtist>()
                .HasKey(aa => new { aa.AlbumId, aa.ArtistId });

            modelBuilder.Entity<AlbumArtist>()
                .HasOne(aa => aa.Artist)
                .WithMany(ar => ar.AlbumArtists)
                .HasForeignKey(aa => aa.ArtistId);

            modelBuilder.Entity<AlbumArtist>()
                .HasOne(aa => aa.Album)
                .WithMany(al => al.AlbumArtists)
                .HasForeignKey(aa => aa.AlbumId);
        }
    }
}
