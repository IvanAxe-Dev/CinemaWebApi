using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.WebApi.Controllers
{
    
    public class TestController : BaseController
    {
        [Authorize]
        [HttpGet("[action]")]
        public IActionResult AuthorizeTest()
        {
            return Ok("ok");
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("[action]")]
        public IActionResult AdminTest()
        {
            return Ok("ok");
        }
        [Authorize(Roles = "User")]
        [HttpGet("[action]")]
        public IActionResult UserTest()
        {
            return Ok("ok");
        }

    }
}
