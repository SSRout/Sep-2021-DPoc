using DPoc.Efcore;
using DPoc.Efcore.Entities;
using DPoc.Efcore.Repositories;
using EntityFrameworkCore.Testing.Moq;
using InnoTech.VideoApplication2021.Domain.IRepositories;
using InnotTech.VideoApplication2021.Core.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.DA.Test.Repositories
{
    public class VideoRepositoryTest
    {
        private readonly VideoApplicationDbContext _fackeContext;

        public VideoRepositoryTest()
        {
            _fackeContext = Create.MockedDbContextFor<VideoApplicationDbContext>();
        }
        public class Comparer : IEqualityComparer<Video>
        {
            public bool Equals(Video x, Video y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(null, y)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Id == y.Id && x.Title == y.Title && x.StoryLine == y.StoryLine && x.ReleaseDate == y.ReleaseDate && x.Genre == y.Genre;
            }

            public int GetHashCode([DisallowNull] Video obj)
            {
                return HashCode.Combine(obj.Id, obj.Title, obj.StoryLine, obj.ReleaseDate, obj.Genre);
            }
        }

        [Fact]
        public void VideoRepository_IsVideoRepository()
        {
            var videoRepo = new VideoRepository(_fackeContext);
            Assert.IsAssignableFrom<IVideoRepository>(videoRepo);
        }

        [Fact]
        public void VideoRepository_WithNullDbContext_ThrowInValidDataException()
        {
            Assert.Throws<InvalidDataException>(()=>new VideoRepository(null));
        }

        [Fact]
        public void VideoRepository_WithNullDbContext_ThrowExceptionMessage()
        {
           var exception= Assert.Throws<InvalidDataException>(() => new VideoRepository(null));
            Assert.Equal("Must Have DbContext", exception.Message);
        }

        [Fact]
        public void FindAll_GetAllVideosEntityInDbContext_AsListOfVideos()
        {
            var list = new List<VideoEntity>
            {
                new VideoEntity{Id=1,Title="test1",StoryLine="",ReleaseDate=DateTime.Now,Genre=null },
                new VideoEntity{Id=2,Title="test2",StoryLine="",ReleaseDate=DateTime.Now,Genre=null }
            };
            var repo = new VideoRepository(_fackeContext);
            //Integration
            _fackeContext.Set<VideoEntity>().AddRange(list);
            _fackeContext.SaveChanges();

            var expectedList = list.Select(vl => new Video {Id=vl.Id,Title=vl.Title,StoryLine=vl.StoryLine, ReleaseDate=vl.ReleaseDate}).ToList();

            //Act
            var actualList = repo.FindAll();
            //Assert
            Assert.Equal(expectedList, actualList, new Comparer());
        }
    }
}
