using DPoc.Efcore;
using DPoc.Efcore.Entities;
using EntityFrameworkCore.Testing.Moq;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace Infrastructure.DA.Test
{
    public class VideoApplicationDbContextTest
    {
        private readonly VideoApplicationDbContext _mockDbContext;

        public VideoApplicationDbContextTest()
        {
            _mockDbContext = Create.MockedDbContextFor<VideoApplicationDbContext>();
        }
        [Fact]
        public void DbContext_WithDbContextOptions_IsAvailable()
        {
           
            Assert.NotNull(_mockDbContext);
        }

        [Fact]
        public void DbContext_Dbsets_MustHaveDbSetWithTypeVideoEntity()
        {
            Assert.True(_mockDbContext.Videos is DbSet<VideoEntity>);
        }

        [Fact]
        public void DbContext_Dbsets_MustHaveDbSetWithTypeGenreEntity()
        {
            Assert.True(_mockDbContext.Genres is DbSet<GenreEntity>);
        }
    }
}
