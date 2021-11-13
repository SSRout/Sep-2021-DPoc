using InnoTech.VideoApplication2021.Domain.IRepositories;
using InnoTech.VideoApplication2021.Domain.Services;
using InnotTech.VideoApplication2021.Core.IServices;
using InnotTech.VideoApplication2021.Core.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace Domain.Test.Services
{
    public class VideoServiceTest
    {
        private readonly Mock<IVideoRepository> _mock;
        private readonly VideoService _service;

        public VideoServiceTest()
        {
            _mock= new Mock<IVideoRepository>();
            _service= new VideoService(_mock.Object);
        }

        [Fact]
        public void VideoService_IsIVideoService()
        {
            Assert.True(_service is IVideoService);
        }

        [Fact]
        public void VideoService_WithNullvideoRepository_ThrowInvalidDataException()
        {
            Assert.Throws<InvalidDataException>(()=>new VideoService(null));
        }

        [Fact]
        public void GetVideos_CallVideoRepositoryFindAll_ExactlyOnce()
        {
            _service.ReadAll();
            _mock.Verify(r => r.FindAll(), Times.Once);
        }

        [Fact]
        public void GetVideos_NoFilter_ReturnListOfAllVideos()
        {
            var expected = new List<Video>
            {
                new Video{Id=1,Genre=null,ReleaseDate=DateTime.Now,Title="test1",StoryLine=""},
                new Video{Id=2,Genre=null,ReleaseDate=DateTime.Now,Title="test2",StoryLine=""}
            };
            _mock.Setup(r => r.FindAll()).Returns(expected);
            var actual = _service.ReadAll();
            Assert.Equal(expected, actual);
        }
    }
}
