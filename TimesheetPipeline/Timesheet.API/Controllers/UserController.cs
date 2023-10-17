using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Timesheet.Application.Mappers;
using Timesheet.Domain.Entities.Users;
using Timesheet.Domain.Exceptions;
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

        #region Create
        [HttpPost("Subscribe/Free")]
        public async Task<IActionResult> FreeSubscription(UserAddForm form)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return Ok($"{await _service.AddAsync(form.ToEntity())}");
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

        [HttpPost("Subscribe/Premium")]
        public async Task<IActionResult> PremiumSubscription(UserAddForm form)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return Ok($"{await _service.AddAsync(form.ToEntity(), RoleType.Premium)}");
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

        [Authorize(Roles = "Admin")]
        [HttpPost("AddNewAdministrator")]
        public async Task<IActionResult> AddNewAdmin(UserAddForm form)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return Ok($"{await _service.AddAsync(form.ToEntity(), RoleType.Admin)}");
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

        [HttpPost("Login")]
        [AllowAnonymous]
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
                return BadRequest(ModelState);
            }
        }
        #endregion

        #region Read
        [Authorize("Admin")]
        [HttpGet("Get")]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
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
        public async Task<ActionResult<User>> GetById(Guid id)
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
        #endregion

        #region Update/Patch
        [Authorize("Admin")]
        [HttpPut("UpdateUserInfo/{userIdToUpdate}")]
        public async Task<IActionResult> UpdateUser(Guid userIdToUpdate, UserUpdateForm form)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return Ok($"{await _service.UpdateAsync(form.ToEntity(userIdToUpdate))}");
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

        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "Regular")]
        [HttpPatch("BecomePremium/{userIdToUpdate}")]
        public async Task<IActionResult> BecomePremium(Guid userIdToUpdate)
        {
            try
            {
                return Ok($"{await _service.UpdateAsync(userIdToUpdate, RoleType.Premium)}");
            }
            catch(UselessUpdateException ex)
            {
                return BadRequest(ex);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize("Admin")]
        [Authorize("Premium")]
        [HttpPatch("BecomeRegular/{userIdToUpdate}")]
        public async Task<IActionResult> BecomeRegular(Guid userIdToUpdate)
        {
            try
            {
                return Ok($"{await _service.UpdateAsync(userIdToUpdate, RoleType.Regular)}");
            }
            catch (UselessUpdateException ex)
            {
                return BadRequest(ex);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        #endregion

        #region Delete
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
        #endregion
    }
}