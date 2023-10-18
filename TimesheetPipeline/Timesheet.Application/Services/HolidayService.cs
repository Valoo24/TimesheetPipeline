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
            if (id <= 0 || id > 10) throw new ArgumentOutOfRangeException($"{id}");

            return await _repository.GetByIdAsync(id);
        }

        /// <summary>
        /// Méthode asynchrone permettant de renvoyer un IEnumerable de tous les entity Holiday présents dans la base de donnée pour un mois en particulier.
        /// </summary>
        /// <param name="year">L'Année à attribuer aux entity Holiday.</param>
        /// <param name="month">Le mois des entity Holiday à récupérer.</param>
        /// <exception cref="NonExistingMonthException"></exception>
        /// <exception cref="NoContentException"></exception>
        public async Task<IEnumerable<Holiday>> GetByMonthAsync(int year, int month)
        {
            if (month <= 0 || month > 12) throw new NonExistingMonthException(month);

            IEnumerable<Holiday> holidayList = await _repository.GetAllAsync();

            holidayList = holidayList.Where(h => h.Date.Month == month);

            if(!holidayList.Any()) throw new NoContentException(holidayList);

            foreach (var holiday in holidayList)
            {
                ChangeDate(holiday, year);
            }

            return holidayList;
        }

        /// <summary>
        /// Transforme la date de l'entity Holiday selon l'année donnée.
        /// </summary>
        /// <param name="holiday">Entity Holiday à transformer.</param>
        /// <param name="year">Année de l'entity Holiday.</param>
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
        /// <summary>
        /// Renvoie la date du dimanche de Pâques.
        /// </summary>
        /// <param name="year">Année du dimanche de Pâques.</param>
        /// <param name="getMondayDate">Si true, renvoie le lundi de Pâques (paramètre par défaut), sinon, renvoie le dimanche de Pâques</param>
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
    }
}