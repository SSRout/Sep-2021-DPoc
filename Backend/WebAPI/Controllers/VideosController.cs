using InnotTech.VideoApplication2021.Core.IServices;
using InnotTech.VideoApplication2021.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Dtos.Videos;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideosController : ControllerBase
    {
        private IVideoService _service;
        public VideosController(IVideoService repo)
        {
            _service = repo;
        }

        [HttpPost]
        public  ActionResult<Video> CreateVideo([FromBody]PostVideoDto VideoDto)
        {
            var newVideo = new Video
            {
                Title = VideoDto.Title,
                ReleaseDate = VideoDto.ReleaseDate,
                StoryLine = VideoDto.StoryLine
            };
            return Ok(_service.Create(newVideo));
        }

        [HttpGet]
        public ActionResult<List<Video>> Get()
        {
            return Ok(_service.ReadAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Video> GetById(int id)
        {
            var video= _service.ReadById(id);

            return Ok(new GetVideoByIdDto
            {
                Title=video.Title,
                StoryLine=video.StoryLine,
                ReleaseDate=video.ReleaseDate
            });
        }
    }
}
