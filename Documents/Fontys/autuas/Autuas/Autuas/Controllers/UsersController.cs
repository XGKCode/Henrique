using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Autuas.Data;
using Autuas.Dtos;
using Autuas.Helpers;
using Autuas.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
//test
namespace Autuas.Controllers
{
    [Authorize]
    [Route("")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UsersController(IUserRepo userRepository, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _userRepo = userRepository;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Authenticate([FromBody] AuthenticateUserModel model)
        {
            var user = _userRepo.Authenticate(model.Username, model.Password);

            if (user == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserID.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new
            {
                UserId = user.UserID,
                Name = user.Name,
                Username = user.Username,
                Email = user.Email,
                Token = tokenString
            });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] UserCreateDto userCreateDto)
        {
            //map model to entity
            var user = _mapper.Map<User>(userCreateDto);

            try
            {
                //create user
                _userRepo.CreateUser(user, userCreateDto.Password);
                return Ok();
            }
            catch (AppException ex)
            {
                //return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }


        //GET api/[controller]
        [HttpGet("/")]
        public ActionResult<IEnumerable<UserReadDto>> GetAllUsers()
        {
            var users = _userRepo.GetAllUsers();
            var model = _mapper.Map<IEnumerable<UserReadDto>>(users);
            //returns 200 succes code
            return Ok(model);

        }
        //GET api/[controller]/{id}
        [HttpGet("/users/{id}", Name = "GetUserByID")]
        public ActionResult<UserReadDto> GetUserByID(int id)
        {
            var user = _userRepo.GetUserByID(id);
            if (user != null)
            {
                return Ok(_mapper.Map<UserReadDto>(user));
            }
            return NotFound();
        }

        //Post api/[controller]
        [HttpPost]
        public ActionResult<UserReadDto> CreateUser(UserCreateDto userCreateDto)
        {
            var userModel = _mapper.Map<User>(userCreateDto);
            _userRepo.CreateUser(userModel);
            _userRepo.SaveChanges();

            var userReadDto = _mapper.Map<UserReadDto>(userModel);
            //route name(api/users)          // route value       //content - content value to format in entity body               
            return CreatedAtRoute(nameof(GetUserByID), new { Id = userReadDto.UserID }, userReadDto);
        }
        //PUT api/[controller]/{id}
        [HttpPut("/{id}")]
        public ActionResult UpdateUser(int id, UserUpdateDto userUpdateDto)
        {
            var userModelFromRepo = _userRepo.GetUserByID(id);
            if (userModelFromRepo == null)
            {
                return NotFound();
            }

            //maps the user object with the new values, to the user object that has been previously created and updates them
            _mapper.Map(userUpdateDto, userModelFromRepo);

            _userRepo.UpdateUser(userModelFromRepo);
            _userRepo.SaveChanges();

            return NoContent();
        }
        /*  [HttpPut("{id}")]
          public IActionResult Update(int id, [FromBody] UserUpdateDto userUpdate)
          {
              //map model to entity and set id
              var user = _mapper.Map<User>(userUpdate);
              user.UserID = id;

              try
              {
                  //update user
                  _userRepo.UpdateUser(user, userUpdate.Password);
                  return Ok();
              }
              catch (AppException ex)
              {
                  return BadRequest(new { message = ex.Message });
              }
          }*/
        //PATCH api/[controller]/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialUserUpdate(int id, JsonPatchDocument<UserUpdateDto> patchDoc)
        {
            var userModelFromRepo = _userRepo.GetUserByID(id);
            if (userModelFromRepo == null)
            {
                return NotFound();
            }
            var userToPatch = _mapper.Map<UserUpdateDto>(userModelFromRepo);
            patchDoc.ApplyTo(userToPatch, ModelState);

            // validate check
            if (!TryValidateModel(userToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(userToPatch, userModelFromRepo);
            _userRepo.UpdateUser(userModelFromRepo);
            _userRepo.SaveChanges();
            //204 - for successful changes
            return NoContent();
        }
        //DELETE api/[controller]/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            var userModelFromRepo = _userRepo.GetUserByID(id);
            if (userModelFromRepo == null)
            {
                return NotFound();
            }
            _userRepo.DeleteUser(userModelFromRepo);
            _userRepo.SaveChanges();
            return NoContent();
        }
        /* [HttpDelete("{id}")]
         public IActionResult Delete(int id)
         {
             _userRepo.DeleteUser(id);
             return Ok();
         }*/


    }
}
