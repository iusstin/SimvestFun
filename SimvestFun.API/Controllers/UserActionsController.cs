using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimvestFun.ApplicationCore.Interfaces;
using SimvestFun.ApplicationCore.Models;

namespace SimvestFun.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserActionsController : BaseController
    {
        private readonly IUserActionService _userActionService;
        private readonly IMapper _mapper;

        public UserActionsController(IUserActionService userActionService, IMapper mapper, IJwtUtils jwtUtils): base(jwtUtils)
        {
            _userActionService = userActionService;
            _mapper = mapper;
        }

        [HttpGet("{userId}/{numberOfActions}")]
        public async Task<ActionResult<ActionsModel>> GetUserActions(string userId, int numberOfActions)
        {
            var actions =  await _userActionService.GetUserActionsAsync(userId, numberOfActions);

            return actions;
        }


        [HttpGet("last-action")]
        public async Task<ActionResult<UserActionModel>> GetLastActionByConnectedUser()
        {
            var loggedUserId = GetConnectedUserId();

            var userAction = await _userActionService.GetLastUserActionAsync(loggedUserId);

            if (userAction == null)
                return NotFound();
            else
                return _mapper.Map<UserActionModel>(userAction);
        }


        [HttpGet]
        public async Task<ActionResult<List<UserActionModel>>> GetRecentUserActions()
        {
            var actions = await _userActionService.GetRecentUserActionsAsync();
            return _mapper.Map<List<UserActionModel>>(actions);
        }

        [HttpGet("last-bonus")]
        public async Task<ActionResult> GetLastBonusAction()
        {
            var loggedUserId = GetConnectedUserId();
            var hasReceivedBonus = await _userActionService.CheckIfUserHasReceivedBonusToday(loggedUserId);
            return Ok(hasReceivedBonus);
        }
    }
}
