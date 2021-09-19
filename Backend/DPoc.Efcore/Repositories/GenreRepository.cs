using InnoTech.VideoApplication2021.Domain.IRepositories;
using InnotTech.VideoApplication2021.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPoc.Efcore.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly VideoApplicationDbContext _ctx;
        public GenreRepository(VideoApplicationDbContext ctx)
        {

            _ctx = ctx;
        }
        public Genre Create(Genre genre)
        {
            var res = _ctx.Genres.Add(new Entities.GenreEntity { Name = genre.Name }).Entity;
            _ctx.SaveChanges();
            return new Genre
            {
                Id = res.Id,
                Name = res.Name
            };
        }

        public Genre Delete(int id)
        {
            var res = _ctx.Genres.Remove(new Entities.GenreEntity { Id = id }).Entity;
            _ctx.SaveChanges();
            return new Genre { Id = res.Id };
        }

        public List<Genre> GetAll()
        {
            return _ctx.Genres.Select(genrsEntity => new Genre { Id = genrsEntity.Id, Name = genrsEntity.Name }).ToList();
        }

        public Genre GetById(int id)
        {
            var res = _ctx.Genres.FirstOrDefault(gns => gns.Id == id);
            if (res != null)
                return new Genre { Id = res.Id, Name = res.Name };
            return null;
        }

        public Genre Update(Genre genre)
        {
            var res = _ctx.Genres.Update(new Entities.GenreEntity { Id = genre.Id, Name = genre.Name }).Entity;
            _ctx.SaveChanges();
            return new Genre
            {
                Name = res.Name
            };
        }
    }
}
