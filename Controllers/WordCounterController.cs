using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WordCounterApi.Data;
using WordCounterApi.Models;

namespace WordCounterApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WordCounterController : ControllerBase
    {
        private readonly AppDbContext _context;

        public WordCounterController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<TextStats>> CountWordsAndCharacters([FromBody] TextInput input)
        {
            if (string.IsNullOrWhiteSpace(input.Text))
            {
                return BadRequest("Text cannot be empty.");
            }

            var wordCount = input.Text.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
            var charCount = input.Text.Replace(" ", "").Length;

            var analysis = new TextAnalysis
            {
                Text = input.Text,
                WordCount = wordCount,
                CharacterCount = charCount
            };

            _context.TextAnalyses.Add(analysis);
            await _context.SaveChangesAsync();

            return Ok(new TextStats
            {
                WordCount = wordCount,
                CharacterCount = charCount
            });
        }
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<TextAnalysis>>> GetAllAnalyses()
        {
            return await _context.TextAnalyses
                                 .OrderByDescending(t => t.CreatedAt)
                                 .ToListAsync();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnalysis(int id)
        {
            var analysis = await _context.TextAnalyses.FindAsync(id);
            if (analysis == null)
                return NotFound($"No analysis found with ID {id}");

            _context.TextAnalyses.Remove(analysis);
            await _context.SaveChangesAsync();

            return NoContent(); // 204
        }



    }
}

