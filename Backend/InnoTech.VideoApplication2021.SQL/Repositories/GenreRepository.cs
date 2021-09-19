using InnoTech.VideoApplication2021.Domain.IRepositories;
using InnoTech.VideoApplication2021.SQL.Converters;
using InnoTech.VideoApplication2021.SQL.Entities;
using InnotTech.VideoApplication2021.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InnoTech.VideoApplication2021.SQL.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private static List<GenreEntity> _genresTable = new List<GenreEntity>();
        private static int _id = 1;
        private readonly GenreConverter _genreConverter;
        public GenreRepository()
        {
            _genreConverter = new GenreConverter();
        }
        public Genre Create(Genre genre)
        {
            var genreEntity = _genreConverter.Convert(genre);
            genreEntity.Id = _id++;
            _genresTable.Add(genreEntity);
            return _genreConverter.Convert(genreEntity);

        }

        public Genre Delete(int id)
        {
            throw  new  NotImplementedException();
        }

        public List<Genre> GetAll()
        {
            var genreList = new List<Genre>();
            foreach (var res in _genresTable)
            {
                genreList.Add(_genreConverter.Convert(res));
            }
            return genreList;

        }

        public Genre GetById(int id)
        {
            return _genreConverter.Convert(_genresTable.FirstOrDefault(x => x.Id == id));
        }

        public Genre Update(Genre grnre)
        {
            throw new NotImplementedException();
        }
    }
}
