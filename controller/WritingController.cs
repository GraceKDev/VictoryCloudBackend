using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VictoryCloudApi.Data;
using VictoryCloudApi.Models;

namespace VictoryCloudApi.Controller
{
    [Authorize]
    [ApiController]
    [Route("Api/[controller]")]
    public class WritingController : ControllerBase
    {
        private readonly MyDbContext _context;

        public WritingController(MyDbContext myDbContext)
        {
            _context = myDbContext;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateWritingDto dto)
        {
            if (dto == null)
                return BadRequest("Writing data is required.");

            if (string.IsNullOrWhiteSpace(dto.Title))
                return BadRequest("Title is required.");

            var writing = new Writing
            {
                Title = dto.Title,
                Description = dto.Description,
                Tags = dto.Tags,
                Links = dto.Links,
                UploadedAt = dto.UploadedAt,
                UpdatedAt = dto.UpdatedAt,
                Chapters = dto.Chapters.Select(c => new WritingChapter
                {
                    WritingChapterTitle = c.ChapterTitle,
                    UploadedAt = c.UpdatedAt,
                    UpdatedAt = c.UpdatedAt,
                    WritingChapterContent = c.Content.Select(wcc => new WritingChapterContent
                    {
                        WritingContentPosition = wcc.ContentPosition,
                        WritingContentType = wcc.ContentType,
                        WritingContentBlock = wcc.Content == null ? [] :
                        [
                            new WritingChapterContentBlock
                            {
                                WritingContentBlockContent = wcc.Content.Content,
                                WritingContentBlockImageUrl = wcc.Content.ImageUrl,
                                WritingContentBlockAltText = wcc.Content.AltText,
                            }
                        ],
                    }).ToList(),
                }).ToList(),
            };

            _context.Writing.Add(writing);
            await _context.SaveChangesAsync();
            return Ok(new { writing.WritingId });
        }
        [AllowAnonymous]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var writings = await _context.Writing
                .Include(w => w.Chapters)
                    .ThenInclude(c => c.WritingChapterContent)
                        .ThenInclude(wcc => wcc.WritingContentBlock)
                .ToListAsync();
            return Ok(writings);
        }
        [AllowAnonymous]
        [HttpGet("Get/{writingId}")]
        public async Task<IActionResult> Get(int writingId)
        {
            var writing = await _context.Writing
                .Include(w => w.Chapters)
                    .ThenInclude(c => c.WritingChapterContent)
                        .ThenInclude(wcc => wcc.WritingContentBlock)
                .FirstOrDefaultAsync(w => w.WritingId == writingId);
            if (writing == null)
                return NotFound($"Writing with Id {writingId} not found.");

            return Ok(writing);
        }

        [HttpPut("Update/{writingId}")]
        public async Task<IActionResult> Update(int writingId, [FromBody] CreateWritingDto dto)
        {
            if (dto == null)
                return BadRequest("Writing data is required.");

            var writing = await _context.Writing
                .Include(w => w.Chapters)
                    .ThenInclude(c => c.WritingChapterContent)
                .FirstOrDefaultAsync(w => w.WritingId == writingId);

            if (writing == null)
                return NotFound($"Writing with Id {writingId} not found.");

            writing.Title = dto.Title;
            writing.UploadedAt = dto.UploadedAt;
            writing.UpdatedAt = dto.UpdatedAt;
            writing.Description = dto.Description;
            writing.Tags = dto.Tags;
            writing.Links = dto.Links;

            await _context.SaveChangesAsync();
            return Ok(writing);
        }

        [HttpDelete("Delete/{writingId}")]
        public async Task<IActionResult> Delete(int writingId)
        {
            var writing = await _context.Writing.FindAsync(writingId);
            if (writing == null)
                return NotFound($"Writing with Id {writingId} not found.");

            _context.Writing.Remove(writing);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Writing deleted.", writingId });
        }


        [HttpPost("{writingId}/Chapter/Create")]
        public async Task<IActionResult> CreateChapter(int writingId, [FromBody] WritingChapterDto dto)
        {
            if (dto == null)
                return BadRequest("Chapter data is required.");

            if (string.IsNullOrWhiteSpace(dto.ChapterTitle))
                return BadRequest("Chapter title is required.");

            var writing = await _context.Writing.FindAsync(writingId);
            if (writing == null)
                return NotFound($"Writing with Id {writingId} not found.");

            var chapter = new WritingChapter
            {
                WritingId = writingId,
                UpdatedAt = dto.UpdatedAt,
                UploadedAt = dto.UploadedAt,
                WritingChapterTitle = dto.ChapterTitle,
            };

            _context.WritingChapter.Add(chapter);
            await _context.SaveChangesAsync();
            return Ok(new { chapter.WritingChapterId });
        }

        [HttpDelete("{writingId}/Chapter/Delete/{chapterId}")]
        public async Task<IActionResult> DeleteChapter(int writingId, int chapterId)
        {
            var chapter = await _context.WritingChapter
                .FirstOrDefaultAsync(c => c.WritingChapterId == chapterId && c.WritingId == writingId);

            if (chapter == null)
                return NotFound($"Chapter with Id {chapterId} not found.");

            _context.WritingChapter.Remove(chapter);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Chapter deleted.", chapterId });
        }

        [HttpPost("{writingId}/Chapter/{chapterId}/Content/Create")]
        public async Task<IActionResult> CreateContent(int writingId, int chapterId, [FromBody] WritingChapterContentDto dto)
        {
            if (dto == null)
                return BadRequest("Content data is required.");

            var chapter = await _context.WritingChapter
                .FirstOrDefaultAsync(c => c.WritingChapterId == chapterId && c.WritingId == writingId);

            if (chapter == null)
                return NotFound($"Chapter with Id {chapterId} not found.");

            var content = new WritingChapterContent
            {
                WritingChapterId = chapterId,
                WritingContentPosition = dto.ContentPosition,
                WritingContentType = dto.ContentType,
            };

            _context.WritingChapterContent.Add(content);
            await _context.SaveChangesAsync();
            return Ok(new { content.WritingChapterContentId });
        }

        [HttpDelete("{writingId}/Chapter/{chapterId}/Content/Delete/{contentId}")]
        public async Task<IActionResult> DeleteContent(int writingId, int chapterId, int contentId)
        {
            var content = await _context.WritingChapterContent
                .FirstOrDefaultAsync(c => c.WritingChapterContentId == contentId && c.WritingChapterId == chapterId);

            if (content == null)
                return NotFound($"Content with Id {contentId} not found.");

            _context.WritingChapterContent.Remove(content);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Content deleted.", contentId });
        }
    }
}
