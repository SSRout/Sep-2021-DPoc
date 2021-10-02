using DPoc.Efcore.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace DPoc.Efcore
{
    public class VideoApplicationDbContext : DbContext
    {

        public VideoApplicationDbContext(DbContextOptions<VideoApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VideoEntity>().HasOne(v => v.Genre).WithMany()
                .HasForeignKey(v=>v.GenreId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<GenreEntity>().HasData(new GenreEntity { Id = 1, Name = "Action" });
            modelBuilder.Entity<GenreEntity>().HasData(new GenreEntity { Id = 2, Name = "Biopic" });

            modelBuilder.Entity<VideoEntity>().HasData(new VideoEntity { Id = 1, Title = "Avengers",StoryLine= "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English. Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy. Various versions have evolved over the years, sometimes by accident, sometimes on purpose (injected humour and the like).", ReleaseDate=DateTime.Now,GenreId=1 });
            modelBuilder.Entity<VideoEntity>().HasData(new VideoEntity { Id = 2, Title = "Ms Dhoni", StoryLine = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.", ReleaseDate = DateTime.Now, GenreId = 2 });
        }
        public DbSet<GenreEntity> Genres { get; set; }
        public DbSet<VideoEntity> Videos { get; set; }
    }
}
