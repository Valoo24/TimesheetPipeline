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
        public async Task<IActionResult> CreateNewTimesheet(TimesheetCreateForm form)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _service.AddAsync(form.ToEntity()));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [Authorize("Auth")]
        [HttpPut("AddOccupation/{idToUpdate}")]
        public async Task<IActionResult> AddOccupation(Guid idToUpdate, Occupation form)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _service.UpdateAsync(new TimesheetEntity
                {
                    Id = idToUpdate,
                    OccupationList = new List<Occupation>
                    {
                        form
                    }
                }));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [Authorize("Auth")]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await _service.DeleteAsync(id));
        }

        [Authorize("Admin")]
        [HttpGet("Get")]
        public async Task<ActionResult<IEnumerable<TimesheetEntity>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [Authorize("Auth")]
        [HttpGet("Get/{id}")]
        public async Task<ActionResult<TimesheetEntity>> GetById(Guid id)
        {
            return Ok(await _service.GetByIdAsync(id));
        }

        //-------------------------------------------------------
        // A N'utiliser que lorsqe la db doit être ré-initialisée
        //-------------------------------------------------------

        //[HttpPost("CreateDb")]
        //public async Task<IActionResult> CreateDb()
        //{
        //    await _service.InitializeDatabaseAsync();
        //    return Ok("La base de donnée a bien été crée et initialisée.");
        //}
    }
}