using InnoTech.VideoApplication2021.Domain.IRepositories;
using InnoTech.VideoApplication2021.Domain.Services;
using InnotTech.VideoApplication2021.Core.IServices;
using InnotTech.VideoApplication2021.Core.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Domain.Test.Services
{
    public class GenreServiceTest
    {
        private readonly Mock<IGenreRepository> _mock;
        private readonly GenreService _service;

        public GenreServiceTest()
        {
            _mock = new Mock<IGenreRepository>();
            _service = new GenreService(_mock.Object);
        }

        [Fact]
        public void GenreService_IsIVideoService()
        {
            Assert.True(_service is IGenreServices);
        }

        [Fact]
        public void GenreService_WithNullvideoRepository_ThrowInvalidDataException()
        {
            Assert.Throws<InvalidDataException>(() => new GenreService(null));
        }

        [Fact]
        public void GetGenres_CallVideoRepositoryFindAll_ExactlyOnce()
        {
            _service.GetAll();
            _mock.Verify(r => r.GetAll(), Times.Once);
        }

        [Fact]
        public void GetGenres_NoFilter_ReturnListOfAllVideos()
        {
            var expected = new List<Genre>
            {
                new Genre{Id=1,Name=""},
                new Genre{Id=2,Name=""}
            };
            _mock.Setup(r => r.GetAll()).Returns(expected);
            var actual = _service.GetAll();
            Assert.Equal(expected, actual);
        }
    }
}
