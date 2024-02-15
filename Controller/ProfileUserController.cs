using Microsoft.AspNetCore.Mvc;
using SportLize.Profile.Api.Profile.Business.Abstraction;
using SportLize.Profile.Api.Profile.Shared.Dto;

namespace SportLize.Profile.Api.Controller
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ProfileUserController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IBusiness _business;

        public ProfileUserController(IBusiness business, ILogger<ProfileUserController> logger)
        {
            _business = business;
            _logger = logger;
        }

        [HttpPost(Name = "InsertUser")]
        public async Task<ActionResult> InsertUser(UserWriteDto userWriteDto) =>
            Ok(await _business.InsertUser(userWriteDto));

        [HttpPost(Name = "InsertFollowerToUser")]
        public async Task<ActionResult> InsertFollowerToUser(int userId, int followerId) =>
            Ok(await _business.InserFollowerToUser(userId, followerId));

        [HttpPost(Name = "InsertFollowingToUser")]
        public async Task<ActionResult> InsertFollowingToUser(int userId, int followingId) =>
            Ok(await _business.InsertFollowingToUser(userId, followingId));

        [HttpPut(Name = "UpdateUser")]
        public async Task<ActionResult> UpdateUser(UserReadDto userReadDto) =>
            Ok(await _business.UpdateUser(userReadDto));

        [HttpGet(Name = "GetAllUser")]
        public async Task<ActionResult> GetAllUser() =>
            Ok(await _business.GetAllUser());

        [HttpGet(Name = "GetAllFollowerOfUser")]
        public async Task<ActionResult> GetAllFollowerOfUser(int userId) =>
            Ok(await _business.GetAllFollowerOfUser(userId));

        [HttpGet(Name = "GetAllFollowingOfUser")]
        public async Task<ActionResult> GetAllFollowingOfUser(int userId) =>
            Ok(await _business.GetAllFollowingOfUser(userId));

        [HttpGet(Name = "GetUser")]
        public async Task<ActionResult> GetUser(int id) =>
            Ok(await _business.GetUser(id));

        [HttpDelete(Name = "DeleteUser")]
        public async Task<ActionResult> DeleteUser(UserReadDto userReadDto) =>
             Ok(await _business.DeleteUser(userReadDto));
    }

}
