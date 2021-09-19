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
        private VideoApplicationDbContext _ctx;

        public VideoRepository(VideoApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        public Video Add(Video video)
        {
            throw new NotImplementedException();
        }

        public Video Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Video> FindAll()
        {
            throw new NotImplementedException();
        }

        public Video FindVideoById(int id)
        {
            throw new NotImplementedException();
        }

        public Video UpdateVideo(Video video)
        {
            throw new NotImplementedException();
        }
    }
}
