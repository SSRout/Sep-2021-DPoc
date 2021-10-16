using InnotTech.VideoApplication2021.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Core.Test.Models
{
    public class VideoTestcs
    {
        private Video _video;
        public VideoTestcs()
        {
            _video = new Video();
        }
        [Fact]
        public void Video_CanBeInitialized()
        {
            Assert.NotNull(_video);
        }

        [Fact]
        public void Video_NameIsString()
        {
            _video.Title = "Check me";
            Assert.Contains("Check me", _video.Title);
        }

        [Fact]
        public void Video_IdMustInt()
        {
            Assert.True(_video.Id is int);
        }
    }
}
