using CodeBookAPL.Models.DTO;
using CodeBookAPL.Repository.IRepository;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CodeBookAPL.Models;

namespace CodeBookAPL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserRepository _userRepo;
        //protected APIResponse _apiResponse;

        public UserController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
            //this._apiResponse = new();


        }

        [HttpPost("login")]

        public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
        {
            var loginResponse = await _userRepo.Login(model);

            if (loginResponse.user == null || string.IsNullOrEmpty(loginResponse.Token))
            {
                //_apiResponse.StatusCode = HttpStatusCode.BadRequest;
                //_apiResponse.IsSuccess = false;

                //_apiResponse.ErrorMessage.Add("username or password is incorrect");
                //return BadRequest(_apiResponse);
                return BadRequest(ModelState);
            }

            //_apiResponse.StatusCode = HttpStatusCode.OK;
            //_apiResponse.IsSuccess = true;

            //_apiResponse.Result = loginResponse;
            //return Ok(_apiResponse);

            return Ok(loginResponse);
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDTO model)
        {
            bool ifUserEmailUnique = _userRepo.IsUniqueEmail(model.Email);

            if (!ifUserEmailUnique)
            {
                //_apiResponse.StatusCode = HttpStatusCode.BadRequest;
                //_apiResponse.IsSuccess = false;

                //_apiResponse.ErrorMessage.Add("email already exist");
                //return BadRequest(_apiResponse);

                return BadRequest(ModelState);

            }

            var user = await _userRepo.Registration(model);

            if (user == null)
            {
                //_apiResponse.StatusCode = HttpStatusCode.BadRequest;
                //_apiResponse.IsSuccess = false;

                //_apiResponse.ErrorMessage.Add("registration failed");
                //return BadRequest(_apiResponse);

                return BadRequest(ModelState);
            }

            //_apiResponse.StatusCode = HttpStatusCode.OK;

            return Ok(user);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {

            var user = await _userRepo.GetAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }
           
            return Ok(user);
        }

    }
}
