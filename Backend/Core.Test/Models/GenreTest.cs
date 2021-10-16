using InnotTech.VideoApplication2021.Core.Models;
using System;
using Xunit;

namespace Core.Test
{
    public class GenreTest
    {
        [Fact]
        public void Genre_CanBeInitialized()
        {
            var gnr = new Genre();
            Assert.NotNull(gnr);
        }

        [Fact]
        public void Set_GenreProp()
        {
            var gnr = new Genre();
            gnr.Id = 1;
            gnr.Name = "test";
            Assert.Equal(1, gnr.Id);
            Assert.Equal("test", gnr.Name);
        }
    }
}
