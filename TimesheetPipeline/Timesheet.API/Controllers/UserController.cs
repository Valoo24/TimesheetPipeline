using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Timesheet.Domain.Entities.Users;
using Timesheet.Domain.Interfaces;

namespace Timesheet.API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UserController : Controller
    {
        private IUserService _service;
        private ITokenManager _tokenManager;
        public UserController(IUserService Service, ITokenManager tokenManager)
        {
            _service = Service;
            _tokenManager = tokenManager;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(UserAddForm form)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return Ok($"{await _service.AddAsync(form)}");
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
        [HttpPut("Update/{userIdToUpdate}")]
        public async Task<IActionResult> Update(Guid userIdToUpdate, UserUpdateForm form)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return Ok($"{await _service.UpdateAsync(userIdToUpdate, form)}");
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
        [HttpDelete("Delete/{userIdToDelete}")]
        public async Task<IActionResult> Delete(Guid userIdToDelete)
        {
            try
            {
                return Ok($"{await _service.DeleteAsync(userIdToDelete)}");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize("Admin")]
        [HttpGet("Get")]
        public async Task<IActionResult> GetAll()
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
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                return Ok(await _service.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginForm form)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    User loginUser = await _service.LoginAsync(form);
                    loginUser.Token = _tokenManager.GenerateToken(loginUser);
                    return Ok(loginUser.Token);
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
    }
}