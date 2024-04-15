using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimvestFun.ApplicationCore.ApplicationExceptions;
using SimvestFun.ApplicationCore.Entities;
using SimvestFun.ApplicationCore.Interfaces;
using SimvestFun.ApplicationCore.Models;

namespace SimvestFun.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IFollowService _followService;


        public UsersController(IUserService userService, IMapper mapper, IJwtUtils jwtUtils, IFollowService followService): base(jwtUtils)
        {
            _userService = userService;
            _mapper = mapper;
            _followService = followService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> GetUserById(string id)
        {
            var result = await _userService.GetById(id);
            var user = _mapper.Map<UserModel>(result);
            return Ok(user);
        }

        [HttpGet]
        public async Task<ActionResult<List<UserModel>>> GetLeaderboard(int count = 20)
        {
            var result = await _userService.GetLeaderboardUsers(count);
            var users = _mapper.Map<List<UserModel>>(result);
            return users;
        }

        [HttpGet("paging/{pageIndex}/{name?}")]
        public async Task<ActionResult<UserPagingModel>> GetUsersByName(int pageIndex, string? name = null)
        {
            var result = await _userService.GetUsersByPageIndex(pageIndex, name);
            return Ok(result);
        }

        [HttpGet("connected")]
        public async Task<ActionResult<UserModel>> GetConnectedUser()
        {
            var userId = GetConnectedUserId();
            if (userId == null)
                return Ok(null);

            var user = await _userService.GetById(userId);
            var userModel = _mapper.Map<UserModel>(user);
            return Ok(userModel);
        }

        [HttpPost("reset-account")]
        public async Task<ActionResult<UserModel>> ResetAccount()
        {
            var loggedUserId = GetConnectedUserId();

            try
            {
                var newUser = await _userService.ResetAccountAsync(loggedUserId);
                var userModel = _mapper.Map<UserModel>(newUser);
                return Ok(userModel);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}/portfolioValues")]
        public async Task<ActionResult<UserModel>> GetUserPortfolioValues(string id)
        {
            try
            {
                var result = await _userService.GetUserPortofolioValues(id);
                return Ok(result);
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPut("update-account")]
        public async Task<ActionResult<UserModel>> UpdateUserDetails([FromBody] UserModel userModel)
        {
            try
            {
                if (userModel.Id != GetConnectedUserId())
                    return Forbid();

                return await _userService.UpdateUserDetails(userModel);
            }
            catch (InvalidOperationException)
            {
                return BadRequest();
            }
        }

        [HttpPut("update-account-by-admin")]
        public async Task<ActionResult<UserModel>> UpdateUserByAdmin([FromBody] UserModel userModel)
        {
            try
            {
                var connectedUserId = GetConnectedUserId();

                return await _userService.UpdateUserDetailsByAdmin(userModel, connectedUserId);
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
            catch (InvalidOperationException)
            {
                return BadRequest();
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
        }

        [HttpPost("daily-bonus")]
        public async Task<ActionResult<UserModel>> AddDailyBonus()
        {
            var loggedUserId = GetConnectedUserId();
            try
            {
                var result = await _userService.AddDailyBonusAsync(loggedUserId);
                return Ok(result);
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
            catch (InvalidActionException)
            {
                return BadRequest();
            }
        }

        [HttpPost("{id}/follow")]
        public async Task<ActionResult> FollowUser(string id)
        {
            var loggedUserId = GetConnectedUserId();
            try
            {
                await _followService.FollowUserAsync(loggedUserId, id);
                return Ok(new { message = "Follow successful" });
            }
            catch (InvalidActionException)
            {
                return Forbid();
            }
        }

        [HttpDelete("{id}/unfollow")]
        public async Task<ActionResult> UnfollowUser(string id)
        {
            var loggedUserId = GetConnectedUserId();
            try
            {
                await _followService.UnfollowUserAsync(loggedUserId, id);
                return Ok(new { message = "Successful unfollow" });
            }
            catch (InvalidActionException)
            {
                return Forbid();
            }
        }

        [HttpGet("{id}/follow")]
        public async Task<ActionResult<Follow>> GetFollowByUser(string id)
        {
            var loggedUserId = GetConnectedUserId();
            return Ok(await _followService.GetFollowByUserAsync(loggedUserId, id));
        }

        [HttpGet("followed-users")]
        public async Task<ActionResult<List<UserModel>>> GetAllFollowedUsers()
        {
            var loggedUserId = GetConnectedUserId();
            var result = await _followService.GetAllFollowedUsersAsync(loggedUserId);
            var mapedUsers = _mapper.Map<List<UserModel>>(result);
            return Ok(mapedUsers);
        }

        [HttpGet("{id}/followers")]
        public async Task<ActionResult<List<UserModel>>> GetFollowersByUser(string id)
        {
            try
            {
                var result = await _followService.GetFollowersByUserAsync(id);
                var mapedUsers = _mapper.Map<List<UserModel>>(result);
                return Ok(mapedUsers);
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPut("unsubscribe/{guid}")]
        public async Task<ActionResult<UserModel>> UnsubscribeUser(string guid)
        {
            try
            {
                var result = await _userService.UnsubscribeUserByGuid(guid);
                return Ok(result);
            }
            catch (EntityNotFoundException)
            {
                return NotFound("No user found");
            }
        }
    }
}
