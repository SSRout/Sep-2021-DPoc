using System.Collections.Generic;
using InnotTech.VideoApplication2021.Core.Models;

namespace InnotTech.VideoApplication2021.Core.IServices
{
    public interface IGenreServices
    {
        Genre Create(Genre genre);
        List<Genre> GetAll();
        Genre GetById(int id);
        Genre Update(Genre grnre);
        Genre Delete(int id);
    }
}
