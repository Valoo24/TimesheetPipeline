using Microsoft.AspNetCore.Mvc;
using Timesheet.Application.Mappers;
using Timesheet.Application.Services;
using Timesheet.Domain.Entities;
using Timesheet.Domain.Entities.Timesheets;

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

        [HttpPost("CreateNewTimesheet")]
        public IActionResult CreateNewTimesheet(TimesheetCreateForm form)
        {
            try
            {
                return Ok(_service.Add(form.ToEntity()));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("AddOccupation/{idToUpdate}")]
        public IActionResult AddOccupation(Guid idToUpdate, Occupation form)
        {
            try
            {
                return Ok(_service.Update(new TimesheetEntity
                {
                    Id = idToUpdate,
                    OccupationList = new List<Occupation>
                    {
                        form
                    }
                }));
            }
            catch (Exception ex) 
            { 
                return NotFound();
            }
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                return Ok(_service.Delete(id));
            }
            catch (Exception ex)
            { 
                return NotFound(ex.Message);
            }
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
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}