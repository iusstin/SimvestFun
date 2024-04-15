using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimvestFun.ApplicationCore.Entities;
using SimvestFun.ApplicationCore.Interfaces;
using SimvestFun.ApplicationCore.Models;
using SimvestFun.ApplicationCore.ApplicationExceptions;

namespace SimvestFun.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersStocksController : BaseController
    {
        private readonly IUserStockService _userStockService;
        private readonly IUserActionService _userActionService;
        private readonly IMapper _mapper;

        public UsersStocksController(IUserStockService userStockService, IUserActionService userActionService, IMapper mapper, IJwtUtils jwtUtils) : base(jwtUtils)
        {
            _userStockService = userStockService;
            _userActionService = userActionService;
            _mapper = mapper;
        }

        [HttpPost("buy")]
        public async Task<ActionResult<UserStock>> BuyUserStock([FromBody] UserStockModel model)
        {
            var loggedUserId = GetConnectedUserId();
            var userStock = _mapper.Map<UserStock>(model);
            if (loggedUserId != userStock.ApplicationUserId)
            {
                return Forbid("Forbidden action");
            }

            try
            {
                await _userStockService.BuyUserStockAsync(userStock);
                await _userActionService.AddUserActionAsync("Buy", model);
                return Ok(new { message = "Purchase successful" });
            }
            catch (InvalidActionException)
            {
                return BadRequest();
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult> GetUserStocksByUserId(string userId)
        {
            var result = await _userStockService.GetUserStocksByUserIdAsync(userId);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetUserStockById(int id)
        {
            var result = await _userStockService.GetUserStockByIdAsync(id);
            return Ok(result);
        }

        [HttpPut("sell/{id:int}")]
        public async Task<ActionResult<UserStock>> SellUserStock([FromBody] UserStockModel model, int id)
        {
            var loggedUserId = GetConnectedUserId();
            var userStock = _mapper.Map<UserStock>(model);
            if (loggedUserId != userStock.ApplicationUserId)
            {
                return Forbid("Forbidden action");
            }

            try
            {
                var result = await _userStockService.SellUserStockAsync(userStock, id);
                await _userActionService.AddUserActionAsync("Sell", model);
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
    }
}
