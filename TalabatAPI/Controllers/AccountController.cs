using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Security.Claims;
using Talabat;
using TalabatAPI.Controllers;

namespace TalabatAPI
{
    public class AccountController : BaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager,ITokenService tokenService,IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) return BadRequest();
            var result = _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if(!result.IsCompleted) return BadRequest();
            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = loginDto.Email,
                //Token = "This will be Token"
                Token = await _tokenService.CreateToken(user,_userManager)
            });
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (CheckEmailExists(registerDto.Email).Result.Value)
                return BadRequest();
            var user = new AppUser()
            {
                Email = registerDto.Email,
                UserName = registerDto.Email.Split("@")[0], //to get name before @
                DisplayName = registerDto.DisplayName,
                PhoneNumber = registerDto.PhoneNumber,
                Address = new Address()
                {
                    FirstName = registerDto.FirstName,
                    LastName = registerDto.LastName,
                    City = registerDto.City,
                    Country = registerDto.Country,
                    Street = registerDto.Street,

                }

            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if(!result.Succeeded) return BadRequest();
            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                //Token = "This will be Token"
                Token = await _tokenService.CreateToken(user, _userManager)
            });
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await _userManager.FindByIdAsync(email);
            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.CreateToken(user, _userManager)
            }
                );
        }

        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            /*            var email = User.FindFirstValue(ClaimTypes.Email);

                        var user = await _userManager.FindByIdAsync(email);

                        return Ok(_mapper.Map<Address,AddressDto>(user.Address));*/

            var user = await _userManager.FindUserWithAddressByEmailAsync(User);

            return Ok(_mapper.Map<Address, AddressDto>(user.Address));
        }

        [Authorize]
        [HttpPut("address")] //update
        public async Task<ActionResult<AddressDto>> UpdateAddress(AddressDto newAdress)
        {
            var user = await _userManager.FindUserWithAddressByEmailAsync(User);

            user.Address = _mapper.Map<AddressDto, Address>(newAdress);

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded) return BadRequest();

            return Ok(newAdress);
        }

        [Authorize]
        [HttpGet("checkExistence")]
        public async Task<ActionResult<bool>> CheckEmailExists(string email)
        {
            return await _userManager.FindByEmailAsync(email) !=null;
        }
    }
}
