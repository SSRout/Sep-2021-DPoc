using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPoc.Efcore.Entities
{
    public class VideoEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string StoryLine { get; set; }
        public int? GenreId { get; set; }//write
        public GenreEntity Genre { get; set; }//read
    }
}
