using InnotTech.VideoApplication2021.Core.IServices;
using InnotTech.VideoApplication2021.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Dtos.Videos;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VideosController : ControllerBase
    {
        private IVideoService _service;
        public VideosController(IVideoService repo)
        {
            if (repo == null)
                throw new InvalidDataException("");
            _service = repo;
        }
      
        [HttpPost]
        public ActionResult<Video> CreateVideo([FromBody] PostVideoDto VideoDto)
        {
            var newVideo = new Video
            {
                Title = VideoDto.Title,
                ReleaseDate = VideoDto.ReleaseDate,
                StoryLine = VideoDto.StoryLine
            };
            return Created($"http://localhost:5000/api/videos/{newVideo.Id}", _service.Create(newVideo));
        }

        [HttpGet]
        public ActionResult<List<Video>> Get()
        {
            return Ok(_service.ReadAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Video> GetById(int id)
        {
            var video = _service.ReadById(id);

            return Ok(new GetVideoByIdDto
            {
                Title = video.Title,
                StoryLine = video.StoryLine,
                ReleaseDate = video.ReleaseDate
            });
        }

        [HttpPut("{id}")]
        public ActionResult<Video> PutVideo(int id,[FromBody] PutVideoDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("id param must be same as object Id..");
            }
            var video = _service.UpdateVideo(new Video
            {
                Id=id,
                Title=dto.Title,
                StoryLine=dto.StoryLine,
                ReleaseDate=dto.ReleaseDate
            });
            return Ok(video);
        }

        [HttpDelete("{id}")]
        public ActionResult<Video> Delete(int id)
        {
            _service.Delete(id);
            return NoContent();
        }
    }
}
