using InnoTech.VideoApplication2021.Domain.IRepositories;
using InnoTech.VideoApplication2021.Domain.Services;
using InnotTech.VideoApplication2021.Core.IServices;
using Moq;
using System;
using System.IO;
using Xunit;

namespace Domain.Test.Services
{
    public class VideoServiceTest
    {
        [Fact]
        public void VideoService_IsIVideoService()
        {
            var mock = new Mock<IVideoRepository>();
            var service = new VideoService(mock.Object);
            Assert.True(service is IVideoService);
        }

        [Fact]
        public void VideoService_WithNullvideoRepository_ThrowInvalidDataException()
        {
            Assert.Throws<InvalidDataException>(()=>new VideoService(null));

        }
    }
}
