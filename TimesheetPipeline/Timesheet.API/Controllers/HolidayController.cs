using Microsoft.AspNetCore.Mvc;
using Timesheet.Application.Services;

namespace Timesheet.API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class HolidayController : Controller
    {
        private HolidayService _service { get; set; }

        public HolidayController(HolidayService Service)
        {
            _service = Service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id) 
        {
            try
            {
                return Ok(_service.GetById(id));
            }
            catch (Exception ex) 
            { 
                return NotFound(ex.Message);
            }
        }
    }
}