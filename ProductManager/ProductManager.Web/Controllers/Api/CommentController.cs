using Microsoft.AspNetCore.Mvc;
using ProductManager.Repositories.Entities;
using ProductManager.Repositories.Interfaces;
using ProductManager.Repositories.Models;
using ProductManager.Services.Interfaces;
using ProductManager.Services.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;

namespace ProductManager.Web.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentsService;

        public CommentController(ICommentService commentsService)
        {
            _commentsService = commentsService;
        }

        // GET: api/Comment
        [HttpGet]
        [SwaggerOperation("GetComments")]
        public async Task<IEnumerable<CommentModel>> Get()
        {
            IEnumerable<CommentModel> comments = await _commentsService.GetComments();
            return comments;
        }

        // GET: api/Comment/{id}
        [HttpGet("{id}")]
        [SwaggerOperation("GetComment")]
        public async Task<CommentModel> Get(int id)
        {
            CommentModel comment = await _commentsService.GetComment(id);

            return comment;
        }

        // POST: api/Comment
        [HttpPost]
        [SwaggerOperation("AddComment")]
        public async Task<bool> Post([FromBody] CommentModel comment)
        {
            await _commentsService.AddComment(comment);

            return true;
        }

        // PUT: api/Comment/{id}
        [HttpPut("{id}")]
        [SwaggerOperation("UpdateComment")]
        public async Task<bool> Put(int id, [FromBody] CommentModel comment)
        {
            await _commentsService.UpdateComment(id, comment);

            return true;
        }

        // DELETE: api/Comment/{id}
        [HttpDelete("{id}")]
        [SwaggerOperation("DeleteComment")]
        public async Task<bool> Delete(int id)
        {
            await _commentsService.DeleteComment(id);

            return true;
        }
    }
}
