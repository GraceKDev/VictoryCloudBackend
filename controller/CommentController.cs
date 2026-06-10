using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VictoryCloudApi.Data;
using VictoryCloudApi.Models;

[Authorize]
[ApiController] 
[Route("Api/[controller]")]
public class CommentController : ControllerBase
{
    private readonly MyDbContext _context;

    public CommentController(MyDbContext myDbContext)
    {
        _context = myDbContext;
    }
   
    [HttpPost("Create/{itemId}")]
    public async Task<IActionResult> Create(int itemId, [FromBody] CommentDto commentDto)
    {
        if(commentDto == null)
        {
            return BadRequest("No data found.");
        }
        var comment = new Comment
        {
            Content = commentDto.Content,
            ContentId = itemId,
            CommentMessage = commentDto.Comment,
            Author = commentDto.Author,
            Date = commentDto.Date,
            Likes = commentDto.Likes,
            ParentId = commentDto.ParentId
        };
        _context.Comment.Add(comment);
        await _context.SaveChangesAsync();
        return Ok(new{comment.CommentId});
    }
    [AllowAnonymous]
    [HttpGet("Get/{itemId}")]
    public async Task<IActionResult> Get(int itemId)
    {
        var comments = await _context.Comment
            .Where(c => c.ContentId == itemId)
            .ToListAsync();
        return Ok(comments);
    }
    
    [HttpDelete("Delete/{commentId}")]
    public async Task<IActionResult> Create(int commentId)
    {

        var comment = await _context.Comment
            .FirstOrDefaultAsync(c => c.CommentId == commentId);
        if(comment == null)
        {
            return NotFound($"Comic with ID {commentId} not found.");
        }
        _context.Comment.Remove(comment);
        await _context.SaveChangesAsync();
        return Ok(new{message="Comment Deleted.",commentId});
    }
}