using Microsoft.AspNetCore.Mvc;
using MyVideos_AspNetCore_Vue.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MyVideos_AspNetCore_Vue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly VideoDBContext _dbContext;

        public VideoController(VideoDBContext dbContext)
        {
            _dbContext = dbContext;
        }



        //Get All Viedos
        [HttpGet]
        public IEnumerable<VideoModel> GetAll() {

            return _dbContext.GetVideo;
        }

        //Single Video
        [HttpGet("{id}")]
        public async Task<ActionResult<VideoModel>> GetById(int id)
        {
            VideoModel video = await _dbContext.GetVideo.FindAsync(id);

            if (video is not null)
            {
                return video;
            }

            return NotFound();
        }

        //Add Video
        [HttpPost]
        public async Task<ActionResult<VideoModel>> Create([FromBody] VideoModel video)
        {
            await _dbContext.GetVideo.AddAsync(video);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = video.Id }, video);
        }

        //Update video
        [HttpPut]
        public async Task<ActionResult> Update([FromBody] VideoModel videoUpdate)
        {
            var result =  _dbContext.GetVideo.Any(x => x.Id == videoUpdate.Id);

                if(!result)
                {
                return NotFound();
                }
                     _dbContext.GetVideo.Update(videoUpdate);
                     await _dbContext.SaveChangesAsync();
                     return Ok();

      
        }
    }
}
