using InnotTech.VideoApplication2021.Core.IServices;
using InnotTech.VideoApplication2021.Core.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Core.Test.IServices
{
    public class IVideoServiceTest
    {
        private IVideoService _videoService;

        public IVideoServiceTest()
        {
            _videoService = new Mock<IVideoService>().Object;
        }
        [Fact]
        public void Is_VideoServiceAvailable()
        {
            Assert.NotNull(_videoService);
        }

        [Fact]
        public void Get_VideosNoParam_ReturnListOfVideos()
        {
            var mock = new Mock<IVideoService>();
            var fake = new List<Video>();
            mock.Setup(s => s.ReadAll())
                .Returns(fake);
            var service = mock.Object;

            Assert.Equal(fake, service.ReadAll());
        }
    }
}
