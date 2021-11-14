using InnotTech.VideoApplication2021.Core.IServices;
using InnotTech.VideoApplication2021.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using WebAPI.Controllers;
using Xunit;

namespace WebApi.Test.Controllers
{
    public class VideosControllerTest
    {
        [Fact]
        public void VideoController_IsofTypeVideosController()
        {
            var videoService = new Mock<IVideoService>();
            var controller = new VideosController(videoService.Object);
            Assert.IsAssignableFrom<VideosController>(controller);
        }

        [Fact]
        public void VideoController_WithNullVideoService_ThrowsInvalidDataException()
        {
            Assert.Throws<InvalidDataException>(() => new VideosController(null));
        }

        [Fact]
        public void VideoController_WithNullVideoService_ThrowExceptionMessage()
        {
            var exception = Assert.Throws<InvalidDataException>(() => new VideosController(null));
            Assert.Equal("", exception.Message);
        }

        [Fact]
        public void VideosController_UseApicontrollerAttribute()
        {
            var typeInfo = typeof(VideosController).GetTypeInfo();
            var attr = typeInfo.GetCustomAttributes().FirstOrDefault(a => a.GetType().Name.Equals("ApiControllerAttribute"));
            Assert.NotNull(attr);
        }

        [Fact]
        public void VideosController_UseRoutAttribute()
        {
            var typeInfo = typeof(VideosController).GetTypeInfo();
            var attr = typeInfo.GetCustomAttributes().FirstOrDefault(a => a.GetType().Name.Equals("RouteAttribute"));
            Assert.NotNull(attr);
        }
        [Fact]
        public void VideosController_UseRoutAttribute_WithParamNameApiController()
        {
            var typeInfo = typeof(VideosController).GetTypeInfo();
            var attr = typeInfo.GetCustomAttributes().FirstOrDefault(a => a.GetType().Name.Equals("RouteAttribute"));
            var param = attr as RouteAttribute;
            Assert.Equal("api/[controller]", param.Template);
        }
        [Fact]
        public void VideosController_HasGetMethod()
        {
            var method = typeof(VideosController).GetMethods().FirstOrDefault(m => "Get".Equals(m.Name));
            Assert.NotNull(method);
        }

        [Fact]
        public void VideosController_HasGetMethod_IsPublic()
        {
            var method = typeof(VideosController).GetMethods().FirstOrDefault(m => "Get".Equals(m.Name));
            Assert.True(method.IsPublic);
        }

        [Fact]
        public void VideosController_HasGetMethod_ReturnActionResultOfListOfVideo()
        {
            var method = typeof(VideosController).GetMethods().FirstOrDefault(m => "Get".Equals(m.Name));
            Assert.Equal(typeof(ActionResult<List<Video>>).FullName,method.ReturnType.FullName);
        }

        [Fact]
        public void Get_HasNoHttpAttribute()
        {
            var methodinfo = typeof(VideosController).GetMethods().FirstOrDefault(m => m.Name == "Get");
            var attr = methodinfo.CustomAttributes.FirstOrDefault(ca => ca.AttributeType.Name == "HttpGetAttribute");
            Assert.NotNull(attr);
        }

        [Fact]
        public void Get_CallVideoServiceGetAll_ExactlyOnce()
        {
            var fakeservice = new Mock<IVideoService>();
            var controller = new VideosController(fakeservice.Object);
            controller.Get();
            fakeservice.Verify(s => s.ReadAll(), Times.Once);
        }
    }
}
