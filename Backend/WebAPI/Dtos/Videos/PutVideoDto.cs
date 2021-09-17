using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Dtos.Videos
{
    public class PutVideoDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string StoryLine { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
