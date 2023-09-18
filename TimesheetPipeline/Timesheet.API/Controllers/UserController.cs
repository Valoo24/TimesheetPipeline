using Microsoft.AspNetCore.Mvc;
using Timesheet.Application.Services;

namespace Timesheet.API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UserController : Controller
    {
        private UserService _service;

        public UserController(UserService Service)
        {
            _service = Service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_service.GetAll());
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id) 
        { 
            try
            {
                return Ok(_service.GetById(id));
            }
            catch(Exception ex) 
            { 
                return NotFound(ex.Message);
            }
        }
    }
}