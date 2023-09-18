using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Domain.Entities;
using Timesheet.Domain.Interfaces;
using Timesheet.Persistence.Repositories;

namespace Timesheet.Application.Services
{
    public class HolidayService : IReaderService<Holiday, int>
    {
        public HolidayRepository _repository { get; set; }

        public HolidayService(HolidayRepository Repository)
        {
            _repository = Repository;
        }

        #region Méthodes Read
        public IEnumerable<Holiday> GetAll()
        {
            return _repository.GetAll();
        }

        public IEnumerable <Holiday> GetAll(int year) 
        { 
            IList<Holiday> HolidayList = _repository.GetAll().ToList();

            foreach(var holiday in HolidayList) 
            { 
                ChangeDate(holiday, year);
            }

            return HolidayList;
        }

        public Holiday GetById(int id)
        {
            return _repository.GetById(id);
        }

        public Holiday GetById(int year, int id) 
        {
            Holiday Holiday = _repository.GetById(id);

            ChangeDate(Holiday, year);

            return Holiday;
        }

        public IEnumerable<Holiday> GetByMonth(int year, int month)
        {
            IList<Holiday> holidayList = this.GetAll().ToList();

            foreach (Holiday holiday in holidayList)
            {
                this.ChangeDate(holiday, year);
            }

            return holidayList.Where(h => h.Date.Month == month);
        }
        #endregion

        #region Méthodes Custom
        public DateTime GetEasterDate(int year, bool getMondayDate = true)
        {
            int a = year % 19;
            int b = year / 100;
            int c = year % 100;
            int d = b / 4;
            int e = b % 4;
            int f = (b + 8) / 25;
            int g = (b - f + 1) / 3;
            int h = (19 * a + b - d - g + 15) % 30;
            int i = c / 4;
            int k = c % 4;
            int l = (32 + 2 * e + 2 * i - h - k) % 7;
            int m = (a + 11 * h + 22 * l) / 451;
            int month = (h + l - 7 * m + 114) / 31;
            int day = ((h + l - 7 * m + 114) % 31);
            DateTime EasterDate = new DateTime(year, month, day);

            if (getMondayDate) return EasterDate.AddDays(2);
            else return EasterDate.AddDays(1);
        }

        public void ChangeDate(Holiday holiday, int year)
        {
            switch (holiday.Id)
            {
                case 2:
                    holiday.Date = GetEasterDate(year);
                    break;
                case 4:
                    holiday.Date = GetEasterDate(year, false).AddDays(40);
                    break;
                case 5:
                    holiday.Date = GetEasterDate(year).AddDays(50);
                    break;
                default:
                    holiday.Date = new DateTime(year, holiday.Date.Month, holiday.Date.Day);
                    break;
            }
        }
        #endregion
    }
}