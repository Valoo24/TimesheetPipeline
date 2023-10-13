using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Timesheet.Application.Mappers;
using Timesheet.Domain.Entities;
using Timesheet.Domain.Entities.Timesheets;
using Timesheet.Domain.Interfaces;

namespace Timesheet.API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class TimesheetController : Controller
    {
        private ITimesheetService _service;

        public TimesheetController(ITimesheetService Service)
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

        [Authorize("Admin")]
        [HttpGet("Get")]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_service.GetAllDTO());
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
                return Ok(_service.GetDTOById(id));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}