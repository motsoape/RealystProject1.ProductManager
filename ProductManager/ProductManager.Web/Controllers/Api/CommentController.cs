using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<CommentController> _logger;

        public CommentController(ICommentService commentsService, ILogger<CommentController> logger)
        {
            _commentsService = commentsService;
            _logger = logger;
        }

        // GET: api/Comment
        [HttpGet]
        [SwaggerOperation("GetComments")]
        public async Task<IEnumerable<CommentModel>> Get()
        {
            try { 
                IEnumerable<CommentModel> comments = await _commentsService.GetComments();
                return comments;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to Get Comments", ex);
                throw;
            }
        }

        // GET: api/Comment/{id}
        [HttpGet("{id}")]
        [SwaggerOperation("GetComment")]
        public async Task<CommentModel> Get(int id)
        {
            try { 
                CommentModel comment = await _commentsService.GetComment(id);

                return comment;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to Get Comment", ex);
                throw;
            }
        }

        // POST: api/Comment
        [HttpPost]
        [SwaggerOperation("AddComment")]
        public async Task<bool> Post([FromBody] CommentModel comment)
        {
            try {
                if (comment == null)
                    throw new ArgumentNullException(nameof(comment), "Cannot be Null or Empty");

                await _commentsService.AddComment(comment);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to Add Comment", ex);
                throw;
            }
        }

        // PUT: api/Comment/{id}
        [HttpPut("{id}")]
        [SwaggerOperation("UpdateComment")]
        public async Task<bool> Put(int id, [FromBody] CommentModel comment)
        {
            try {
                if (comment == null)
                    throw new ArgumentNullException(nameof(comment), "Cannot be Null or Empty");

                await _commentsService.UpdateComment(id, comment);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to Update Comment", ex);
                throw;
            }
        }

        // DELETE: api/Comment/{id}
        [HttpDelete("{id}")]
        [SwaggerOperation("DeleteComment")]
        public async Task<bool> Delete(int id)
        {
            try { 
                await _commentsService.DeleteComment(id);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to Delete Comment", ex);
                throw;
            }
        }
    }
}
