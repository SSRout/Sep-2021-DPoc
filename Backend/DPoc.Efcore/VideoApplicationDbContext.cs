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
            modelBuilder.Entity<GenreEntity>().HasData(new GenreEntity { Id = 1, Name = "Action" });
            modelBuilder.Entity<GenreEntity>().HasData(new GenreEntity { Id = 2, Name = "Biopic" });

            modelBuilder.Entity<VideoEntity>().HasData(new VideoEntity { Id = 1, Title = "Avengers",StoryLine="jew kjewjrewj r ewrkjr",ReleaseDate=DateTime.Now,GenreEntityId=1 });
            modelBuilder.Entity<VideoEntity>().HasData(new VideoEntity { Id = 2, Title = "Ms Dhoni", StoryLine = "jdsfd jkdsfje lkejwjfkew", ReleaseDate = DateTime.Now, GenreEntityId = 2 });
        }
        public DbSet<GenreEntity> Genres { get; set; }
        public DbSet<VideoEntity> Videos { get; set; }
    }
}
