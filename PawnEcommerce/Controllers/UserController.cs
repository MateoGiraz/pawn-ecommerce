using Microsoft.AspNetCore.Mvc;
using PawnEcommerce.DTO;
using Service.User;

namespace PawnEcommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpPost]
        public IActionResult SignUp([FromBody] UserCreateModel newUser)
        {
            _userService.SignUp(newUser.ToEntity());
            return Ok();
        }
        
        [HttpPut]
        public IActionResult Update([FromBody] UserCreateModel updateUser)
        {
            _userService.UpdateUser(updateUser.ToEntity());
            return Ok();
        }
        
        [HttpDelete]
        public IActionResult Delete([FromBody] UserCreateModel deleteUser)
        {
            _userService.DeleteUser(deleteUser.ToEntity());
            return Ok();
        }
    }
}