using Microsoft.AspNetCore.Mvc;
using ProductManager.Repositories.Entities;
using ProductManager.Repositories.Interfaces;
using ProductManager.Services.Interfaces;
using System.Collections.Generic;

namespace ProductManager.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class CommentController : Controller
    {
        private readonly ILogger<CommentController> _logger;
        private readonly ICommentService _commentsService;

        public CommentController(ICommentService commentsService, ILogger<CommentController> logger) 
        { 
            _logger = logger;
            _commentsService = commentsService;
        }

        // GET: api/Comment
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<Comment> comments = await _commentsService.GetComments();
            return Ok(comments);
        }

        // GET: api/Comment/{id}
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(int id)
        {
            Comment comment = await _commentsService.GetComment(id);
            if (comment == null)
            {
                return NotFound("The comment record couldn't be found.");
            }
            return Ok(comment);
        }

        // POST: api/Comment
        [HttpPost]
        public IActionResult Post([FromBody] Comment comment)
        {
            if (comment == null)
            {
                return BadRequest("Comment is null.");
            }
            _commentsService.AddComment(comment);

            return CreatedAtRoute( "Get", new { Id = comment.CommentID }, comment);
        }

        // PUT: api/Comment/{id}
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Comment comment)
        {
            if (comment == null)
            {
                return BadRequest("Comment is null.");
            }

            _commentsService.UpdateComment(id, comment);
            return NoContent();
        }

        // DELETE: api/Comment/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _commentsService.DeleteComment(id);
            return NoContent();
        }
    }
}
