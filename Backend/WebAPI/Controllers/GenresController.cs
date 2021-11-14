using InnotTech.VideoApplication2021.Core.IServices;
using InnotTech.VideoApplication2021.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private IGenreServices _genreServices;
        public GenresController(IGenreServices genreServices)
        {
            if (genreServices == null)
                throw new InvalidDataException("");
            _genreServices = genreServices;
        }

        [HttpGet]
        public ActionResult<List<Genre>> GetAll()
        {
            return Ok(_genreServices.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Genre> GetById(int id)
        {
            return Ok(_genreServices.GetById(id));
        }

        [HttpPost]
        public ActionResult<Genre> Create([FromBody] Genre genre)
        {
            return Ok(_genreServices.Create(genre));
        }

        [HttpDelete("{id}")]
        public ActionResult<Genre> Delete(int id)
        {
            return Ok(_genreServices.Delete(id));
        }

        [HttpPut]
        public ActionResult<Genre> Update([FromBody]Genre genre,int id)
        {
            if (id != genre.Id)
                return BadRequest("Id param should match with obj id");
            return Ok(_genreServices.Update(genre));
        }

    }
}
