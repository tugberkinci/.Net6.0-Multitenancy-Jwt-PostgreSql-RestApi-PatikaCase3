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
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly IMapper _mapper;
        private readonly IAccountHelper _accountHelper;


        public PersonController(IPersonService personService, IMapper mapper, IAccountHelper accountHelper)
        {
            _personService = personService;
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
        [ProducesResponseType(typeof(GenericResponse<IEnumerable<Person>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GenericResponse<IEnumerable<Person>>), StatusCodes.Status401Unauthorized)]

        public async Task<IActionResult> GetAll()
        {
            var person = await Task.Run(() => _personService.GetAll());
            GenericResponse<IEnumerable<Person>> response = new GenericResponse<IEnumerable<Person>>();
            response.Success = true;
            response.Message = null;
            response.Data = person;

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
        [ProducesResponseType(typeof(GenericResponse<Person>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GenericResponse<Person>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(GenericResponse<IEnumerable<Person>>), StatusCodes.Status401Unauthorized)]

        public async Task<IActionResult> GetById()
        {
            var current = _accountHelper.GetCurrentUser();
            var person = await Task.Run(() => _personService.GetByAccountId(current.Id));
            GenericResponse<Person> response = new GenericResponse<Person>();

            if (person == null)
            {
                response.Success = false;
                response.Message = "Does not exist.";
                response.Data = null;
                return NotFound(response);
            }
            response.Success = true;
            response.Message = null;
            response.Data = person;
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
        /// <response code="401">Returns error</response>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(GenericResponse<Person>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(GenericResponse<Person>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(GenericResponse<Person>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(GenericResponse<IEnumerable<Person>>), StatusCodes.Status401Unauthorized)]

        public async Task<IActionResult> Post(PersonDto model)
        {
            var current = _accountHelper.GetCurrentUser();
            
            GenericResponse<Person> response = new GenericResponse<Person>();
            var entity = _mapper.Map<PersonDto, Person>(model);

            entity.AccountId = current.Id;

            if (!ValidationHelper.IsValidEmail(model.Email) && !ValidationHelper.IsPhoneNumber(model.Phone))
            {
                response.Success = false;
                response.Message = "Maill adress or phone number is invalid.";
                response.Data = null;
                return BadRequest(response);
            }


            var result = await Task.Run(() => _personService.Add(entity));

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
        [ProducesResponseType(typeof(GenericResponse<Person>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GenericResponse<Person>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(GenericResponse<Person>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(GenericResponse<IEnumerable<Person>>), StatusCodes.Status401Unauthorized)]

        public async Task<IActionResult> Patch(PersonDto model)
        {
            var current = _accountHelper.GetCurrentUser();
            GenericResponse<Person> response = new GenericResponse<Person>();
            var person = await Task.Run(() => _personService.GetByAccountId(current.Id));
            if (person == null)
            {
                response.Success = false;
                response.Message = "Does not exist. Please check id.";
                response.Data = null;
                return NotFound(response);
            }

            var entity = _mapper.Map<PersonDto, Person>(model);

            if (!ValidationHelper.IsValidEmail(model.Email) && !ValidationHelper.IsPhoneNumber(model.Phone))
            {
                response.Success = false;
                response.Message = "Maill adress or phone number is invalid.";
                response.Data = null;
                return BadRequest(response);
            }

            var result = await Task.Run(() => _personService.Add(entity));

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
        /// <response code="401">Returns error</response>
        [HttpPut]
        [Authorize]
        [ProducesResponseType(typeof(GenericResponse<Person>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(GenericResponse<Person>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(GenericResponse<Person>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(GenericResponse<IEnumerable<Person>>), StatusCodes.Status401Unauthorized)]

        public async Task<IActionResult> Put(PersonDto model)
        {
            var current = _accountHelper.GetCurrentUser();
            GenericResponse<Person> response = new GenericResponse<Person>();
            var entity = _mapper.Map<PersonDto, Person>(model);
            entity.AccountId = current.Id;

            if (!ValidationHelper.IsValidEmail(model.Email) && !ValidationHelper.IsPhoneNumber(model.Phone))
            {
                response.Success = false;
                response.Message = "Maill adress or phone number is invalid.";
                response.Data = null;
                return BadRequest(response);
            }


            var result = await Task.Run(() => _personService.Add(entity));

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
        [ProducesResponseType(typeof(GenericResponse<Person>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GenericResponse<Person>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(GenericResponse<IEnumerable<Person>>), StatusCodes.Status401Unauthorized)]

        public async Task<IActionResult> Delete()
        {
            var current = _accountHelper.GetCurrentUser();
            GenericResponse<String> response = new GenericResponse<String>();
            var person = await Task.Run(() => _personService.GetByAccountId(current.Id));
            if (person == null)
            {
                response.Success = false;
                response.Message = "Does not exist.";
                response.Data = null; ;
                return NotFound(response);
            }

            var result = await Task.Run(() => _personService.Delete(person.Id));
            
            if (result == null)
            {
                response.Success = false;
                response.Message = "Does not exist.";
                response.Data = null; ;
                return NotFound(response);
            }

            response.Success = true;
            response.Message = result;
            response.Data = null;
            return Ok(response);


        }

    }
}
