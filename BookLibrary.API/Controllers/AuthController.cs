using BookLibrary.Application.Dtos;
using BookLibrary.Application.Services;
using BookLibrary.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BookLibrary.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly TokenService _tokenService;

        public AuthController(UserManager<AppUser> userManager, TokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto request)
        {
            var user = new AppUser
            {
                UserName = request.UserName,
                Email = request.Email,
                FullName = request.FullName

            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {

                return BadRequest(result.Errors.Select(a => a.Description));
            }

            // Role ekleme 
            //await _userManager.AddToRoleAsync(user,"User");
            return Ok("Register created successfully");
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto request)
        {
            var user=await _userManager.FindByNameAsync(request.UserName);
            if (user == null) return Unauthorized("User not found");
            var result = await _userManager.CheckPasswordAsync(user,request.Password);
            if (!result) return Unauthorized("Password is wrong");
            var token = _tokenService.CreateToken(user);
            return Ok(new { 
            token
            
            });
        }
        [Authorize]
        [HttpGet("me")]
        public IActionResult GetMe()
        {

            return Ok(new
            {

                UserName = User.FindFirst(ClaimTypes.Name)!.Value
            });

        }


    }
}
