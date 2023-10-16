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
                try
                {
                    return Ok(await _service.AddAsync(form.ToEntity()));
                }
                catch (Exception ex)
                {
                    return NotFound(ex.Message);
                }
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
                try
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
                catch (Exception ex)
                {
                    return NotFound(ex.Message);
                }
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
            try
            {
                return Ok(await _service.DeleteAsync(id));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize("Admin")]
        [HttpGet("Get")]
        public async Task<ActionResult<IEnumerable<TimesheetEntity>>> GetAll()
        {
            try
            {
                return Ok(await _service.GetAllAsync());
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize("Auth")]
        [HttpGet("Get/{id}")]
        public async Task<ActionResult<TimesheetEntity>> GetById(Guid id)
        {
            try
            {
                return Ok(_service.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        //-------------------------------------------------------
        // A N'utiliser que lorsqe la db doit être ré-initialisée
        //-------------------------------------------------------

        //[HttpPost("CreateDb")]
        //public async Task<IActionResult> CreateDb()
        //{
        //    try
        //    {
        //        await _service.InitializeDatabaseAsync();
        //        return Ok("La base de donnée a bien été crée et initialisée.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
    }
}