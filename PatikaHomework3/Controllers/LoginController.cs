using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PatikaHomework3.Data.Model;
using PatikaHomework3.Dto.Dto;
using PatikaHomework3.Dto.Response;
using PatikaHomework3.Helpers;
using PatikaHomework3.Helpers.JwtHelper;
using PatikaHomework3.Service.IServices;

namespace PatikaHomework3.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private IJwtService _jwtService;
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;

        public LoginController(IJwtService jwtService, IMapper mapper,IAccountService accountService)
        {
            _jwtService = jwtService;
            _mapper = mapper;
            _accountService = accountService;

        }


        /// <summary>
        /// Login
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="201">Retuns data </response>
        /// <response code="400">Returns error</response>
        [ProducesResponseType(typeof(GenericResponse<Account>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(GenericResponse<Account>), StatusCodes.Status400BadRequest)]
        [Route("/Login")]
        [HttpPost]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            GenericResponse<AuthenticateResponse> response = new GenericResponse<AuthenticateResponse>();
            var result = _jwtService.Authenticate(model);

            if (result == null)
            {
                response.Success= false;
                response.Message = "Username or password is incorrect";
                return BadRequest(response);
            }
            response.Success = true;
            response.Message = "" ;
            response.Data = result;

            return Ok(response);
        }


        /// <summary>
        /// Create Account
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="201">Retuns data </response>
        /// <response code="404">Returns error</response>
        /// <response code="400">Returns error</response>
        [HttpPost]
        [Route("/CreateAccount")]
        [ProducesResponseType(typeof(GenericResponse<Account>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(GenericResponse<Account>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(GenericResponse<Account>), StatusCodes.Status404NotFound)]

        public async Task<IActionResult> Post(AccountDto model)
        {
            GenericResponse<Account> response = new GenericResponse<Account>();
            model.Password = ValidationHelper.GetSha(model.Password);
            var entity = _mapper.Map<AccountDto, Account>(model);

            if (!ValidationHelper.IsValidEmail(model.Email))
            {
                response.Success = false;
                response.Message = "Maill adress invalid.";
                response.Data = null;
                return BadRequest(response);
            }

            var result = await Task.Run(() => _accountService.Add(entity));
            

            if (result == null)
            {
                response.Success = false;
                response.Message = "An error ocurred.";
                response.Data = null;
                return BadRequest(response);
            }
            response.Success = true;
            response.Message = null;
            response.Data = result;

            return Created("", response);


        }
    }
}
