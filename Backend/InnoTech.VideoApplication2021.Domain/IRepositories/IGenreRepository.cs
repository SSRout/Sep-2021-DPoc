using System.Collections.Generic;
using InnotTech.VideoApplication2021.Core.Models;
namespace InnoTech.VideoApplication2021.Domain.IRepositories
{
    public interface IGenreRepository
    {
        Genre Create(Genre genre);
        List<Genre> GetAll();
        Genre GetById(int id);
        Genre Update(Genre grnre);
        Genre Delete(int id);
    }
}
