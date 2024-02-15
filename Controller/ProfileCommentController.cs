using Microsoft.AspNetCore.Mvc;
using SportLize.Profile.Api.Profile.Business.Abstraction;
using SportLize.Profile.Api.Profile.Shared.Dto;

namespace SportLize.Profile.Api.Controller
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ProfileCommentController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IBusiness _business;

        public ProfileCommentController(IBusiness business, ILogger<ProfileCommentController> logger)
        {
            _business = business;
            _logger = logger;
        }

        [HttpPost(Name = "InsertComment")]
        public async Task<ActionResult> InsertCommentForPost([FromQuery] int postId, [FromBody] CommentWriteDto commentWriteDto)
        {      
            return Ok(await _business.InsertCommentForPost(postId, commentWriteDto));
        }

        [HttpPut(Name = "UpdateComment")]
        public async Task<ActionResult> UpdateComment(CommentReadDto commentReadDto)
        {
            return Ok(await _business.UpdateComment(commentReadDto));
        }

        [HttpGet(Name = "GetAllComment")]
        public async Task<ActionResult> GetAllCommentOfPost([FromQuery] int postId)
        {
            return Ok(await _business.GetAllCommentOfPost(postId));
        }

        [HttpGet(Name = "GetComment")]
        public async Task<ActionResult> GetComment(int id)
        {
            return Ok(await _business.GetComment(id));
        }

        [HttpDelete(Name = "DeleteComment")]
        public async Task<ActionResult> DeleteComment(CommentReadDto commentReadDto)
        {
            return Ok(await _business.DeleteComment(commentReadDto));
        }
    }
}
