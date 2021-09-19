using InnoTech.VideoApplication2021.SQL.Entities;
using InnotTech.VideoApplication2021.Core.Models;

namespace InnoTech.VideoApplication2021.SQL.Converters
{
    public class GenreConverter
    {
        public Genre Convert(GenreEntity genreEntity)
        {
            return new Genre
            {
                Id = genreEntity.Id,
                Name = genreEntity.Name,
            };
        }

        public GenreEntity Convert(Genre genre)
        {
            return new GenreEntity
            {
                Id = genre.Id,
                Name = genre.Name,
            };
        }
    }
}
