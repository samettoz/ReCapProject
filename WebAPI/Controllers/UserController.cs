using Business.Abstract;
using Core.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUserSevices _userServices;
        public UsersController(IUserSevices userSevices)
        {
            _userServices = userSevices;
        }

        [HttpPost("getclaims")]
        public IActionResult GetClaims(User user)
        {
           var result = _userServices.GetClaims(user);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
