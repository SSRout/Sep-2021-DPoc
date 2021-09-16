using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Dtos.Videos
{
    public class GetVideoByIdDto
    {
        public string Title { get; set; }
        public string StoryLine { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
