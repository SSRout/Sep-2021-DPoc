using InnoTech.VideoApplication2021.Domain.IRepositories;
using InnotTech.VideoApplication2021.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPoc.Efcore.Repositories
{
    public class VideoRepository : IVideoRepository
    {
        private readonly VideoApplicationDbContext _ctx;

        public VideoRepository(VideoApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        public Video Add(Video video)
        {
            var entity = _ctx.Videos.Add(new Entities.VideoEntity
            {
                Id=video.Id,
                Title = video.Title,
                ReleaseDate = video.ReleaseDate,
                StoryLine = video.StoryLine
            }).Entity;
            _ctx.SaveChanges();
            return new Video()
            {
                Id = entity.Id,
                Title = entity.Title,
                ReleaseDate = entity.ReleaseDate,
                StoryLine = entity.StoryLine,
                Genre = new Genre
                {
                    Id = entity.Genre.Id
                }
            };
        }

        public Video Delete(int id)
        {
            var res = _ctx.Videos.Remove(new Entities.VideoEntity { Id = id }).Entity;
            _ctx.SaveChanges();
            return new Video { Id = res.Id };
        }

        public List<Video> FindAll()
        {
            return _ctx.Videos.Select(videoEntity => new Video { Id = videoEntity.Id, Title = videoEntity.Title,ReleaseDate=videoEntity.ReleaseDate,StoryLine=videoEntity.StoryLine,
                Genre = new Genre
                {
                    Id = videoEntity.Genre.Id,
                    Name = videoEntity.Genre.Name
                }
            }).ToList();
        }

        public Video FindVideoById(int id)
        {
            //var res = _ctx.Videos.FirstOrDefault(video => video.Id == id);
            //if (res != null)
            //    return new Video { Id = res.Id, Title = res.Title, ReleaseDate = res.ReleaseDate, StoryLine = res.StoryLine };
            //return null;

            return _ctx.Videos.Select(entity=>new Video() {
                Id = entity.Id,
                Title = entity.Title,
                ReleaseDate = entity.ReleaseDate,
                StoryLine = entity.StoryLine,
                Genre = new Genre
                {
                    Id = entity.Genre.Id,
                    Name=entity.Genre.Name
                }
            }).FirstOrDefault(video => video.Id == id);
        }

        public Video UpdateVideo(Video video)
        {
            var entity = _ctx.Videos.Update(new Entities.VideoEntity { Id =video.Id, Title = video.Title,ReleaseDate=video.ReleaseDate,StoryLine=video.StoryLine,GenreId=video.Genre.Id }).Entity;
            _ctx.SaveChanges();
            return new Video
            {
                Id = entity.Id,
                Title = entity.Title,
                ReleaseDate = entity.ReleaseDate,
                StoryLine = entity.StoryLine,
                Genre = new Genre
                {
                    Id = entity.Genre.Id
                }
            };
        }
    }
}
