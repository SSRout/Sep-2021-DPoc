using InnotTech.VideoApplication2021.Core.IServices;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Linq;
using System.Reflection;
using WebAPI.Controllers;
using Xunit;

namespace WebApi.Test.Controllers
{
    public class VideosControllerTest
    {
        private readonly IVideoService _videoService;

        public VideosControllerTest()
        {
            _videoService = new Mock<IVideoService>().Object;
        }
        [Fact]
        public void VideoController_IsofTypeVideosController()
        {
            var controller = new VideosController(_videoService);
            Assert.IsAssignableFrom<VideosController>(controller);
        }

        [Fact]
        public void VideosController_UseApicontrollerAttribute()
        {
            var typeInfo = typeof(VideosController).GetTypeInfo();
            var attr = typeInfo.GetCustomAttributes().FirstOrDefault(a => a.GetType().Name.Equals("ApiControllerAttribute"));
            Assert.NotNull(attr);
        }

        [Fact]
        public void VideosController_UseRoutAttribute_WithParamNameApiController()
        {
            var typeInfo = typeof(VideosController).GetTypeInfo();
            var attr = typeInfo.GetCustomAttributes().FirstOrDefault(a => a.GetType().Name.Equals("RouteAttribute"));
            Assert.NotNull(attr);
            var param = attr as RouteAttribute;
            Assert.Equal("api/[controller]", param.Template);
        }
    }
}
