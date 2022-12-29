
using Microsoft.EntityFrameworkCore;


namespace MusicAppApi.Data.Models.Context
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Song>()
                .HasOne<Artist>(s => s.Artist)
                .WithMany(a => a.Songs)
                .HasForeignKey(s => s.ArtistID)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Song>()
                .HasOne<Album>(s => s.Album)
                .WithMany(a => a.Songs)
                .HasForeignKey(s => s.AlbumID)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Song>()
                .HasOne<Genre>(s => s.Genre)
                .WithMany(a => a.Songs)
                .HasForeignKey(s => s.GenreID)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Song> Song { get; set; }
        public DbSet<Album> Album { get; set; }
        public DbSet<Artist> Artist { get; set; }
        public DbSet<Genre> Genre { get; set; }

    }
}
