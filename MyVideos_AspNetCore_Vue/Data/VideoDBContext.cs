using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace MyVideos_AspNetCore_Vue.Data
{
    public class VideoDBContext : DbContext
    {
        public VideoDBContext(DbContextOptions<VideoDBContext> options) : base(options)
        {

        }

        public DbSet<VideoModel> GetVideo { get; set; }
    }
}
