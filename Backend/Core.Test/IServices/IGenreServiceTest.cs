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
    public class IGenreServiceTest
    {
        private IGenreServices _genreService;

        public IGenreServiceTest()
        {
            _genreService= new Mock<IGenreServices>().Object;
        }
        [Fact]
        public void IsGenre_ServiceAvailable()
        {
            Assert.NotNull(_genreService);
        }
    }
}
