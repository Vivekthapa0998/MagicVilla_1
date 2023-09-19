using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MagicVilla_VillaAPI.Controllers
{
    [Route("api/UsersAuth")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepo;
        protected APIResponse _response;
        public UsersController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
            this._response= new();
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
        {
            
                var loginResponse = await _userRepo.Login(model);
                if(loginResponse.User== null || string.IsNullOrEmpty(loginResponse.Token)) 
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages.Add("Username or password is invalid");
                    return BadRequest(_response);

                }
            _response.IsSuccess=true;
            _response.StatusCode=HttpStatusCode.OK;
            _response.Result= loginResponse;
            return Ok(_response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterationRequestDTO model)
        {
            var ifUsernameUnique = _userRepo.IsUniqueUser(model.UserName);
            if(ifUsernameUnique== false)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("Username already exists");
                return BadRequest(_response);
            }
            var user= await _userRepo.Register(model);
            if(user==null)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("Error while registering user");
                return BadRequest(_response);
            }
            _response.IsSuccess=true;
            _response.StatusCode=HttpStatusCode.OK;
            return Ok(_response);

        }

    }
}
