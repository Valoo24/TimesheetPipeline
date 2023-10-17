using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Timesheet.Domain.Entities;
using Timesheet.Domain.Interfaces;

namespace Timesheet.API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    //[ServiceFilter(typeof(AuthInterceptor))]
    public class HolidayController : Controller
    {
        private IHolidayService _service { get; set; }

        public HolidayController(IHolidayService Service)
        {
            _service = Service;
        }

        [Authorize("Auth")]
        [HttpGet("Get/{year}")]
        public async Task<ActionResult<IEnumerable<Holiday>>> GetAll(int year)
        {
            try
            {
                IEnumerable<Holiday> holidayList = await _service.GetAllAsync();

                foreach (var holiday in holidayList) 
                {
                    _service.ChangeDate(holiday, year);
                }

                return Ok(holidayList);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize("Auth")]
        [HttpGet("GetById/{year}/{id}")]
        public async Task<ActionResult<Holiday>> GetById(int year, int id)
        {
            try
            {
                Holiday holiday = await _service.GetByIdAsync(id);

                _service.ChangeDate(holiday, year);

                return Ok(holiday);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize("Auth")]
        [HttpGet("GetByMonth/{year}/{month}")]
        public async Task<ActionResult<IEnumerable<Holiday>>> GetByMonth(int year, int month)
        {
            try
            {
                return Ok(await _service.GetByMonthAsync(year, month));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}