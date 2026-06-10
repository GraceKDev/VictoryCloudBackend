using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VictoryCloudApi.Data;
using VictoryCloudApi.Models;

[ApiController]
[Route("Api/[controller]")]
public class ComicController : ControllerBase
{
    private readonly MyDbContext _context;

    public ComicController(MyDbContext myDbContext)
    {
        _context = myDbContext;
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromBody] CreateComicDto dto)
    {
        if (dto == null)
            return BadRequest("No data found.");

        var comic = new Comic
        {
            Title = dto.Title,
            Description = dto.Description,
            CoverImageUrl = dto.CoverImageUrl,
            Tags = dto.Tags,
            Comments = dto.Comments,
            Details = new ComicDetails
            {
                Status = dto.Details.Status,
                Year = dto.Details.Year,
                OriginalLanguage = dto.Details.OriginalLanguage,
                ContentRating = dto.Details.ContentRating,
            },
            Chapters = dto.Chapters.Select(c => new ComicChapter
            {
                ChapterTitle = c.ChapterTitle,
                Images = c.Images,
            }).ToList(),
        };
        _context.Comics.Add(comic);
        await _context.SaveChangesAsync();
        return Ok(new { comic.ComicId });
    }

    [HttpGet("GetAll")] 
    public async Task<IActionResult> GetAll() {
        List<Comic> comics = await _context.Comics
            .Include(c => c.Details)
            .Include(c => c.Chapters)
            .ToListAsync();
        return Ok(comics);
    }

    [HttpGet("Get/{comicId}")] 
    public async Task<IActionResult> Get(int comicId) {
        Console.WriteLine(comicId);
        var comic = await _context.Comics
            .Include(c => c.Details)
            .Include(c => c.Chapters)
            .FirstOrDefaultAsync(c => c.ComicId == comicId);
        if(comic == null) {
            return NotFound($"comic with Id {comicId} not found.");
        }
        return Ok(comic);
    }

    [HttpPut("Update/{comicId}")]
    public async Task<IActionResult> Update(int comicId, [FromBody] CreateComicDto dto)
    {
        var comic = await _context.Comics
            .Include(c => c.Details)
            .Include(c => c.Chapters)
            .FirstOrDefaultAsync(c => c.ComicId == comicId);

        if (comic == null)
            return NotFound($"Comic with ID {comicId} not found.");

        comic.Title = dto.Title;
        comic.Description = dto.Description;
        comic.CoverImageUrl = dto.CoverImageUrl;
        comic.Tags = dto.Tags;
        comic.Comments = dto.Comments;

        comic.Details.Status = dto.Details.Status;
        comic.Details.Year = dto.Details.Year;
        comic.Details.OriginalLanguage = dto.Details.OriginalLanguage;
        comic.Details.ContentRating = dto.Details.ContentRating;

        _context.ComicChapters.RemoveRange(comic.Chapters);
        comic.Chapters = dto.Chapters.Select(c => new ComicChapter
        {
            ChapterTitle = c.ChapterTitle,
            Images = c.Images,
        }).ToList();

        await _context.SaveChangesAsync();
        return Ok(comic);
    }

    [HttpDelete("Delete/{comicId}")]
    public async Task<IActionResult> Delete(int comicId)
    {
        var comic = await _context.Comics.FindAsync(comicId);
        if (comic == null)
            return NotFound($"Comic with ID {comicId} not found.");

        _context.Comics.Remove(comic);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Comic deleted.", comicId });
    }
}