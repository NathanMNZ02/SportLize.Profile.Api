using Microsoft.AspNetCore.Mvc;
using SportLize.Profile.Api.Profile.Business.Abstraction;
using SportLize.Profile.Api.Profile.Shared.Dto;

namespace SportLize.Profile.Api.Controller
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ProfilePostController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IBusiness _business;

        public ProfilePostController(IBusiness business, ILogger<ProfilePostController> logger)
        {
            _business = business;
            _logger = logger;
        }

        [HttpPost(Name = "InsertPost")]
        public async Task<ActionResult> InsertPostForUser([FromQuery] int userId, [FromBody] PostWriteDto postWriteDto)
        {
            return Ok(await _business.InsertPostForUser(userId, postWriteDto));
        }

        [HttpPut(Name = "UpdatePost")]
        public async Task<ActionResult> UpdatePost(PostReadDto postReadDto)
        {
            return Ok(await _business.UpdatePost(postReadDto));
        }

        [HttpGet(Name = "GetAllPost")]
        public async Task<ActionResult> GetAllPostOfUser(int userId)
        {
            return Ok(await _business.GetAllPostOfUser(userId));
        }

        [HttpGet(Name = "GetPost")]
        public async Task<ActionResult> GetPost(int id)
        {
            return Ok(await _business.GetPost(id));
        }

        [HttpDelete(Name = "DeletePost")]
        public async Task<ActionResult> DeletePost(PostReadDto postReadDto)
        {
            return Ok(await _business.DeletePost(postReadDto));
        }
    }

}
