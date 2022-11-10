using AspWebApi.net.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AspWebApi.net.Controllers
{
    [ApiController]
    [Route("controller")]
    public class AuthController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly ITokenHandler tokenHandler;

        public AuthController(IUserRepository userRepository,ITokenHandler tokenHandler)
        {
            this.userRepository = userRepository;
            this.tokenHandler = tokenHandler;
        }

        [HttpPost]
        [Route("login")]
       public async Task<IActionResult> LoginAsync(Models.DTO.LoginRequest loginRequest)
        {

            //check if user is authenticated

            //check username and password
            var user=await userRepository.AuthenticateAsync(loginRequest.Username,loginRequest.Password);

            if (user!=null)
            {
                //generate a JWT token
                var token=await tokenHandler.CreateTokenAsync(user);
                return Ok(token);
            }
            return BadRequest("Username or Password is incorrect");

        }
    }
}
