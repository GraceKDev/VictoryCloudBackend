using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VictoryCloudApi.Data;
using VictoryCloudApi.Models;



namespace VictoryCloudApi.Controller
{
    [ApiController]
    [Route("Api/[controller]")]
    public class ArtController : ControllerBase
    {
        private readonly MyDbContext _context;
        public ArtController(MyDbContext myDbContext)
        {
            _context = myDbContext;
        }
        [HttpPost("Create")] 
        public async Task<IActionResult> Create([FromBody] Art createArtDto)
        {
            if(createArtDto == null)
            {
                return BadRequest("Art Data is required.");
                
            }
           _context.Art.Add(createArtDto);
           return Ok(new {createArtDto.ArtId});
        }
        [HttpPost("GetAll")] 
        public async Task<IActionResult> GetAll()
        {
           var arts = await _context.Art.ToListAsync();
           return Ok(arts);
        }

        [HttpPost("Get/{artId}")] 
        public async Task<IActionResult> Get(int artId)
        {
           var art = await _context.Art.FirstOrDefaultAsync(a => a.ArtId == artId);
           if(art == null)
            {
                return NotFound($"Art with Id {artId} not found");
            }
           return Ok(art);
        }

        [HttpPut("Update/{artId}")]
        public async Task<IActionResult> Update(int artId,[FromBody] Art createArtDto)
        {
            var art = await _context.Art.FirstOrDefaultAsync(a => a.ArtId == artId);
            if(art == null)
            {
                return NotFound($"Art with Id {artId} not found");
            }
            art.Title = createArtDto.Title;
            art.Description = createArtDto.Description;
            art.ImageUrl = createArtDto.ImageUrl;
            art.Links = createArtDto.Links;
            art.Tags = createArtDto.Tags;            
            await _context.SaveChangesAsync();
            return Ok(art);
        }

        [HttpDelete("Delete/{artId}")]
        public async Task<IActionResult> Delete(int artId)
        {
            var art = await _context.Art.FirstOrDefaultAsync(a => a.ArtId == artId);
            if(art == null)
            {
                return NotFound($"Art with Id {artId} not found");
            }
            _context.Art.Remove(art);
            await _context.SaveChangesAsync();

            return Ok(new {message = "Art Deleted.", artId});
        }
    }
}