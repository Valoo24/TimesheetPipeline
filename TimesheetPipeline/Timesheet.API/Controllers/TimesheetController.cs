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

        [Authorize("Auth")]
        [HttpPost("CreateNewTimesheet")]
        public IActionResult CreateNewTimesheet(TimesheetCreateForm form)
        {
            if (ModelState.IsValid)
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
            else
            {
                return BadRequest("Le formulaire n\'a pas été rempli correctement.");
            }
        }

        [Authorize("Auth")]
        [HttpPut("AddOccupation/{idToUpdate}")]
        public IActionResult AddOccupation(Guid idToUpdate, Occupation form)
        {
            if (ModelState.IsValid)
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
            else
            {
                return BadRequest("Le formulaire n\'a pas été rempli correctement.");
            }
        }

        [Authorize("Auth")]
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

        [Authorize("Auth")]
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