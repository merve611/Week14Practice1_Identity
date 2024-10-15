using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Week14Practice1_Identity.Dtos;
using Week14Practice1_Identity.Services;

namespace Week14Practice1_Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IDataProtector _dataProtector;

        public AuthController(IUserService userService, IDataProtectionProvider dataProtectionProvider)
        {
            _userService = userService;
            _dataProtector = dataProtectionProvider.CreateProtector("security");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            // Şifreyi IDataProtector ile şifreleyelim (encrypt)
            string encryptedPassword = _dataProtector.Protect(request.Password);

            var addUserDto = new AddUserDto
            {
                Email = request.Email,
                Password = encryptedPassword,  // Şifrelenmiş parola
            };

            var result = await _userService.AddUser(addUserDto);

            if (result.IsSucced)
                return Ok(result);
            else
                return BadRequest(result.Message);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var user = await _userService.GetUserByEmail(request.Email);

            if (user == null)
                return Unauthorized("Invalid email or password");

            // Veritabanında saklanan şifreyi çöz (decrypt)
            string decryptedPassword = _dataProtector.Unprotect(user.Password);

            if (decryptedPassword == request.Password)
                return Ok("Login successful");
            else
                return Unauthorized("Invalid email or password");
        }
    }


}
