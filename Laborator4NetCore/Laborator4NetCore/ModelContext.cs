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
                .HasKey(a => new { a.AlbumId, a.ArtistId });

            modelBuilder.Entity<AlbumArtist>()
                .HasOne(a => a.Artist)
                .WithMany(ar => ar.AlbumArtists)
                .HasForeignKey(a => a.ArtistId);

            modelBuilder.Entity<AlbumArtist>()
                .HasOne(a => a.Album)
                .WithMany(al => al.AlbumArtists)
                .HasForeignKey(a => a.AlbumId);
        }
    }
}
