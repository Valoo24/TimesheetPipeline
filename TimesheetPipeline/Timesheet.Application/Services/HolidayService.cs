using Timesheet.Domain.Entities;
using Timesheet.Domain.Exceptions;
using Timesheet.Domain.Interfaces;

namespace Timesheet.Application.Services
{
    public class HolidayService : IHolidayService
    {
        public IHolidayRepository _repository { get; set; }

        public HolidayService(IHolidayRepository Repository)
        {
            _repository = Repository;
        }

        #region Méthodes Read
        public async Task<IEnumerable<Holiday>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Holiday> GetByIdAsync(int id)
        {
            CheckIdRange(id);

            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Holiday>> GetByMonthAsync(int year, int month)
        {
            CheckMonth(month);

            IEnumerable<Holiday> holidayList = await GetAllAsync();

            foreach (var holiday in holidayList)
            {
                ChangeDate(holiday, year);
            }

            return holidayList.Where(h => h.Date.Month == month);
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

        #region Méthodes Custom
        private DateTime GetEasterDate(int year, bool getMondayDate = true)
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
        #endregion

        #region Check Methods
        private void CheckMonth(int month)
        {
            if (month <= 0 || month > 12) throw new NonExistingMonthException(month);
        }

        private void CheckIdRange(int id)
        {
            if (id <= 0 || id > 10) throw new ArgumentOutOfRangeException($"id");
        }
        #endregion
    }
}