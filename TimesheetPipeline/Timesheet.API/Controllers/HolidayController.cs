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

        [HttpGet("Get/{year}")]
        public IActionResult GetAll(int year)
        {
            try
            {
                return Ok(_service.GetAll(year));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("GetById/{year}/{id}")]
        public IActionResult GetById(int year, int id)
        {
            try
            {
                return Ok(_service.GetById(year, id));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("GetByMonth/{year}/{month}")]
        public IActionResult GetByMonth(int year, int month)
        {
            try
            {
                return Ok(_service.GetByMonth(year, month));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}