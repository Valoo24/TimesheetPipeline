using Microsoft.AspNetCore.Mvc;
using Timesheet.Application.Services;

namespace Timesheet.API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class TimesheetController : Controller
    {
        private TimesheetService _service;

        public TimesheetController(TimesheetService Service)
        {
            _service = Service;
        }

        [HttpGet("Get")]
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

        [HttpGet("Get/{id}")]
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