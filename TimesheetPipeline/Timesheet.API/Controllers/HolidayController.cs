using Microsoft.AspNetCore.Mvc;

namespace Timesheet.API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class HolidayController : Controller
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok("Accès à la méthode pour avoir toutes les Vacances.");
        }
    }
}