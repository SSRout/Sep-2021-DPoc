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
    public class GenreControllerTest
    {
        [Fact]
        public void GenresController_IsofTypeGenresController()
        {
           var  service = new Mock<IGenreServices>();
           var  controller = new GenresController(service.Object);
           Assert.IsAssignableFrom<GenresController>(controller);
        }

        [Fact]
        public void GenresController_WithNullGenreService_ThrowsInvalidDataException()
        {
            Assert.Throws<InvalidDataException>(() => new GenresController(null));
        }

        [Fact]
        public void GenresController_WithNullGenreService_ThrowExceptionMessage()
        {
            var exception = Assert.Throws<InvalidDataException>(() => new GenresController(null));
            Assert.Equal("", exception.Message);
        }

        [Fact]
        public void GenresController_UseApicontrollerAttribute()
        {
            var typeInfo = typeof(GenresController).GetTypeInfo();
            var attr = typeInfo.GetCustomAttributes().FirstOrDefault(a => a.GetType().Name.Equals("ApiControllerAttribute"));
            Assert.NotNull(attr);
        }

        [Fact]
        public void GenresController_UseRoutAttribute()
        {
            var typeInfo = typeof(GenresController).GetTypeInfo();
            var attr = typeInfo.GetCustomAttributes().FirstOrDefault(a => a.GetType().Name.Equals("RouteAttribute"));
            Assert.NotNull(attr);
        }
        [Fact]
        public void GenresController_UseRoutAttribute_WithParamNameApiController()
        {
            var typeInfo = typeof(GenresController).GetTypeInfo();
            var attr = typeInfo.GetCustomAttributes().FirstOrDefault(a => a.GetType().Name.Equals("RouteAttribute"));
            var param = attr as RouteAttribute;
            Assert.Equal("api/[controller]", param.Template);
        }
        [Fact]
        public void GenresController_HasGetAllMethod()
        {
            var method = typeof(GenresController).GetMethods().FirstOrDefault(m => "GetAll".Equals(m.Name));
            Assert.NotNull(method);
        }

        [Fact]
        public void GenresController_HasGetAllMethod_IsPublic()
        {
            var method = typeof(GenresController).GetMethods().FirstOrDefault(m => "GetAll".Equals(m.Name));
            Assert.True(method.IsPublic);
        }

        [Fact]
        public void GenresController_HasGetAllMethod_ReturnActionResultOfListOfGenre()
        {
            var method = typeof(GenresController).GetMethods().FirstOrDefault(m => "GetAll".Equals(m.Name));
            Assert.Equal(typeof(ActionResult<List<Genre>>).FullName, method.ReturnType.FullName);
        }

        [Fact]
        public void GetAll_HasNoHttpAttribute()
        {
            var methodinfo = typeof(GenresController).GetMethods().FirstOrDefault(m => m.Name == "GetAll");
            var attr = methodinfo.CustomAttributes.FirstOrDefault(ca => ca.AttributeType.Name == "HttpGetAttribute");
            Assert.NotNull(attr);
        }

        [Fact]
        public void Get_CallServiceGetAll_ExactlyOnce()
        {
            var fakeservice = new Mock<IGenreServices>();
            var controller = new GenresController(fakeservice.Object);
            controller.GetAll();
            fakeservice.Verify(s => s.GetAll(), Times.Once);
        }

    }
}
