using Microsoft.AspNetCore.Mvc;
using SimvestFun.ApplicationCore.Interfaces;

namespace SimvestFun.API.Controllers
{
    public class BaseController : Controller
    {
        private readonly IJwtUtils _jwtUtils;

        public BaseController(IJwtUtils jwtUtils)
        {
            _jwtUtils = jwtUtils;
        }

        public string GetConnectedUserId()
        {
            var token = Request.Headers["Authorization"].FirstOrDefault().Split(" ").Last();
            var id = _jwtUtils.ValidateToken(token);
            return id;
        }
    }
}
