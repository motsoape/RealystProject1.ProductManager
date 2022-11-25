using Microsoft.AspNetCore.Mvc;
using ProductManager.Repositories.Entities;
using ProductManager.Repositories.Interfaces;
using ProductManager.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;

namespace ProductManager.Web.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
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
        [SwaggerOperation("GetComments")]
        public async Task<IEnumerable<Comment>> Get()
        {
            IEnumerable<Comment> comments = await _commentsService.GetComments();
            return comments;
        }

        // GET: api/Comment/{id}
        [HttpGet("{id}")]
        [SwaggerOperation("GetComment")]
        public async Task<Comment> Get(int id)
        {
            Comment comment = await _commentsService.GetComment(id);

            return comment;
        }

        // POST: api/Comment
        [HttpPost]
        [SwaggerOperation("AddComment")]
        public void Post([FromBody] Comment comment)
        {
            _commentsService.AddComment(comment);
        }

        // PUT: api/Comment/{id}
        [HttpPut("{id}")]
        [SwaggerOperation("UpdateComment")]
        public void Put(int id, [FromBody] Comment comment)
        {
            _commentsService.UpdateComment(id, comment);
        }

        // DELETE: api/Comment/{id}
        [HttpDelete("{id}")]
        [SwaggerOperation("DeleteComment")]
        public void Delete(int id)
        {
            _commentsService.DeleteComment(id);
        }
    }
}
