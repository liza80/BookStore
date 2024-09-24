using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApplication15.Models;

namespace WebApplication15.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly BooksStoreContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AuthController(IAuthService authService, BooksStoreContext context, IPasswordHasher<User> passwordHasher)
        {
            _authService = authService;
            _context = context;
            _passwordHasher = passwordHasher;
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == loginDto.Username);
            if (user == null || _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginDto.Password) == PasswordVerificationResult.Failed)
            {
                return Unauthorized(new { message = "Invalid username or password" });
            }

            var token = _authService.GenerateJwtToken(user);
            return Ok(new { token });
        }

        // POST: api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto registerDto)
        {
            var userExists = await _context.Users.AnyAsync(u => u.Username == registerDto.Username);
            if (userExists)
            {
                return BadRequest(new { message = "Username is already taken" });
            }

            var newUser = new User
            {
                Username = registerDto.Username,
                Role = "User",  // Default role for new users
                
            };
            newUser.PasswordHash = _passwordHasher.HashPassword(newUser, registerDto.Password);

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            var token = _authService.GenerateJwtToken(newUser);
            return Ok(new { token });
        }


        // POST: api/auth/adminregister
        [HttpPost("adminregister")]
        public async Task<IActionResult> AdminRegister([FromBody] UserRegisterDto registerDto)
        {
            var userExists = await _context.Users.AnyAsync(u => u.Username == registerDto.Username);
            if (userExists)
            {
                return BadRequest(new { message = "Username is already taken" });
            }

            var newUser = new User
            {
                Username = registerDto.Username,
                Role = "Admin",  

            };
            newUser.PasswordHash = _passwordHasher.HashPassword(newUser, registerDto.Password);

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            var token = _authService.GenerateJwtToken(newUser);
            return Ok(new { token });
        }
    }


}
