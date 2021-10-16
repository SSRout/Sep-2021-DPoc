using InnotTech.VideoApplication2021.Core.IServices;
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
    }
}
