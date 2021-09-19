using InnoTech.VideoApplication2021.Domain.IRepositories;
using InnotTech.VideoApplication2021.Core.IServices;
using InnotTech.VideoApplication2021.Core.Models;
using System.Collections.Generic;

namespace InnoTech.VideoApplication2021.Domain.Services
{
    public class GenreService : IGenreServices
    {
        private IGenreRepository _genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }
        public Genre Create(Genre genre)
        {
            return _genreRepository.Create(genre);
        }

        public Genre Delete(int id)
        {
            return _genreRepository.Delete(id);
        }

        public List<Genre> GetAll()
        {
            return _genreRepository.GetAll();
        }

        public Genre GetById(int id)
        {
            return _genreRepository.GetById(id);
        }

        public Genre Update(Genre grnre)
        {
            return _genreRepository.Update(grnre);
        }
    }
}
