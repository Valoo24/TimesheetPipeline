using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Timesheet.Application.Mappers;
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

        #region Create
        [HttpPost("Subscribe/Free")]
        public async Task<IActionResult> FreeSubscription(UserAddForm form)
        {
            if (ModelState.IsValid)
            {
                return Ok($"{await _service.AddAsync(form.ToEntity())}");
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
                return Ok($"{await _service.AddAsync(form.ToEntity(), RoleType.Premium)}");
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [Authorize("Admin")]
        [HttpPost("AddNewAdministrator")]
        public async Task<IActionResult> AddNewAdmin(UserAddForm form)
        {
            if (ModelState.IsValid)
            {
                return Ok($"{await _service.AddAsync(form.ToEntity(), RoleType.Admin)}");
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginForm form)
        {
            if (ModelState.IsValid)
            {
                User loginUser = await _service.LoginAsync(form);
                loginUser.Token = _tokenManager.GenerateToken(loginUser);
                return Ok(loginUser.Token);
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
            return Ok(await _service.GetAllAsync());
        }

        [Authorize("Auth")]
        [HttpGet("Get/{id}")]
        public async Task<ActionResult<User>> GetById(Guid id)
        {
            return Ok(await _service.GetByIdAsync(id));
        }
        #endregion

        #region Update/Patch
        [Authorize("Admin")]
        [HttpPut("UpdateUserInfo/{userIdToUpdate}")]
        public async Task<IActionResult> UpdateUser(Guid userIdToUpdate, UserUpdateForm form)
        {
            if (ModelState.IsValid)
            {
                return Ok($"{await _service.UpdateAsync(form.ToEntity(userIdToUpdate))}");
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [Authorize("Admin")]
        [Authorize("Regular")]
        [HttpPatch("BecomePremium/{userIdToUpdate}")]
        public async Task<IActionResult> BecomePremium(Guid userIdToUpdate)
        {
            return Ok($"{await _service.UpdateAsync(userIdToUpdate, RoleType.Premium)}");
        }
        #endregion

        #region Delete
        [Authorize("Auth")]
        [HttpDelete("Delete/{userIdToDelete}")]
        public async Task<IActionResult> Delete(Guid userIdToDelete)
        {
            throw new ArgumentNullException();
            return Ok($"{await _service.DeleteAsync(userIdToDelete)}");
        }
        #endregion
    }
}