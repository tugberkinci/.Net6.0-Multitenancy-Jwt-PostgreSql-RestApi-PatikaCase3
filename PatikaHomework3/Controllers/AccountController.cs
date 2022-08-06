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
    [Route("[Controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly IAccountHelper _accountHelper;

        public AccountController(IAccountService accountService, IMapper mapper, IAccountHelper accountHelper)
        {
            _accountService = accountService;
            _mapper = mapper;
            _accountHelper = accountHelper;
        }


        /// <summary>
        /// Get all
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Retuns data </response>
        /// <response code="401">Returns error</response>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(GenericResponse<IEnumerable<Account>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GenericResponse<IEnumerable<Account>>), StatusCodes.Status401Unauthorized)]

        public async Task<IActionResult> GetAll()
        {
            var account = await Task.Run(() => _accountService.GetAll());
            GenericResponse<IEnumerable<Account>> response = new GenericResponse<IEnumerable<Account>>();
            response.Success = true;
            response.Message = null;
            response.Data = account;

            return Ok(response);

        }


        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Retuns data </response>
        /// <response code="404">Returns error</response>
        /// <response code="401">Returns error</response>
        [HttpGet("GetCurrent")]
        [Authorize]
        [ProducesResponseType(typeof(GenericResponse<Account>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GenericResponse<Account>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(GenericResponse<IEnumerable<Account>>), StatusCodes.Status401Unauthorized)]

        public async Task<IActionResult> GetById()
        {
            var current = _accountHelper.GetCurrentUser();
            var account = await Task.Run(() => _accountService.GetById(current.Id));
            GenericResponse<Account> response = new GenericResponse<Account>();

            if (account == null)
            {
                response.Success = false;
                response.Message = "Does not exist.";
                response.Data = null;
                return NotFound(response);
            }
            response.Success = true;
            response.Message = null;
            response.Data = account;
            return Ok(response);

        }


        /// <summary>
        /// post
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="201">Retuns data </response>
        /// <response code="404">Returns error</response>
        /// <response code="400">Returns error</response>
        [HttpPost]
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



        /// <summary>
        /// update
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="200">Retuns data </response>
        /// <response code="404">Returns error</response>
        /// <response code="400">Returns error</response>
        /// <response code="401">Returns error</response>
        [HttpPatch("UpdateCurrent")]
        [Authorize]
        [ProducesResponseType(typeof(GenericResponse<Account>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GenericResponse<Account>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(GenericResponse<Account>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(GenericResponse<IEnumerable<Account>>), StatusCodes.Status401Unauthorized)]

        public async Task<IActionResult> Patch(AccountDto model)
        {
            var current = _accountHelper.GetCurrentUser();
            GenericResponse<Account> response = new GenericResponse<Account>();
            var account = await Task.Run(() => _accountService.GetById(current.Id));
            if (account == null)
            {
                response.Success = false;
                response.Message = "Does not exist. Please check id.";
                response.Data = null;
                return NotFound(response);
            }

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
                response.Message = "An error occured.";
                response.Data = null;
                return BadRequest(response);
            }

            response.Success = true;
            response.Message = null;
            response.Data = result;
            return Ok(response);

        }


        /// <summary>
        /// post
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="201">Retuns data </response>
        /// <response code="404">Returns error</response>
        /// <response code="400">Returns error</response>

        [HttpPut]
        [ProducesResponseType(typeof(GenericResponse<Account>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(GenericResponse<Account>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(GenericResponse<Account>), StatusCodes.Status404NotFound)]

        public async Task<IActionResult> Put(AccountDto model)
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


        /// <summary>
        /// delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Retuns data </response>
        /// <response code="404">Returns error</response>
        /// <response code="401">Returns error</response>
        [HttpDelete("DeleteCurrent")]
        [Authorize]
        [ProducesResponseType(typeof(GenericResponse<Account>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GenericResponse<Account>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(GenericResponse<IEnumerable<Account>>), StatusCodes.Status401Unauthorized)]

        public async Task<IActionResult> Delete()
        {
            var current = _accountHelper.GetCurrentUser();
            var account = await Task.Run(() => _accountService.Delete(current.Id));
            GenericResponse<String> response = new GenericResponse<String>();
            if (account == null)
            {
                response.Success = false;
                response.Message = "Does not exist.";
                response.Data = null; ;
                return NotFound(response);
            }
            response.Success = true;
            response.Message = account;
            response.Data = null;
            return Ok(response);


        }

    }
}
