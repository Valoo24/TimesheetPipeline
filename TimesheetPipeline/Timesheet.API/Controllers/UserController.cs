using Microsoft.AspNetCore.Mvc;
using Timesheet.Application.Services;
using Timesheet.Domain.Entities.Users;

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

        [HttpPost("Add")]
        public IActionResult Add(UserAddForm form)
        {
            try
            {
                return Ok($"{_service.Add(form)}");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("Update/{userIdToUpdate}")]
        public IActionResult Update(Guid userIdToUpdate, UserAddForm form)
        {
            try
            {
                return Ok($"{_service.Update(userIdToUpdate, form)}");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("Delete/{userIdToDelete}")]
        public IActionResult Delete(Guid userIdToDelete)
        {
            try
            {
                return Ok($"{_service.Delete(userIdToDelete)}");
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