﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Timesheet.Domain.Entities;
using Timesheet.Domain.Exceptions;
using Timesheet.Domain.Interfaces;

namespace Timesheet.API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class HolidayController : Controller
    {
        private IHolidayService _service { get; set; }

        public HolidayController(IHolidayService Service)
        {
            _service = Service;
        }

        [Authorize("Auth")]
        [HttpGet("Get/{year}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<IEnumerable<Holiday>>> GetAll(int year)
        {
            IEnumerable<Holiday> holidayList = await _service.GetAllAsync();

            foreach (var holiday in holidayList)
            {
                _service.ChangeDate(holiday, year);
            }

            return Ok(holidayList);
        }

        [Authorize("Auth")]
        [HttpGet("GetById/{year}/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<Holiday>> GetById(int year, int id)
        {
            Holiday holiday = await _service.GetByIdAsync(id);

            _service.ChangeDate(holiday, year);

            return Ok(holiday);
        }

        [Authorize("Auth")]
        [HttpGet("GetByMonth/{year}/{month}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<IEnumerable<Holiday>>> GetByMonth(int year, int month)
        {
            return Ok(await _service.GetByMonthAsync(year, month));
        }
    }
}