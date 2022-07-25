using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAPICRUD_JWT.Data.Interfaces;
using WebAPICRUD_JWT.Dtos;
using WebAPICRUD_JWT.Models;
using WebAPICRUD_JWT.Services.Interfaces;

namespace WebAPICRUD_JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AuthController(IAuthRepository authRepository, ITokenService tokenService, IMapper mapper)
        {
            _authRepository = authRepository;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDTO registerUserDTO)
        {
            registerUserDTO.Email = registerUserDTO.Email.ToLower();
            if (await _authRepository.ExistsUser(registerUserDTO.Email))
                return BadRequest("El correo ingresado ya está en uso");

            var newUser = _mapper.Map<User>(registerUserDTO);
            var createdUser = await _authRepository.Register(newUser, registerUserDTO.Password);
            var ReturnedUserDTO = _mapper.Map<ListUsersDTO>(createdUser);

            return Ok(ReturnedUserDTO); 
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login (LoginUserDTO loginUserDTO)
        {
            var userFromRepo = await _authRepository.Login(loginUserDTO.Email, loginUserDTO.Password);
            if (userFromRepo == null)
                return Unauthorized();

            var userDTO = _mapper.Map<ListUsersDTO>(userFromRepo);

            var token = _tokenService.CreateToken(userFromRepo);

            return Ok(new
            {
                token = token,
                user = userFromRepo
            }) ; 
        }
    }
}
