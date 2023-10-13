using Timesheet.Blazor.FakeDB;
using Timesheet.Domain.Entities;

namespace Timesheet.Blazor.Pages
{
    public partial class HolidayOverview
    {
        public List<Holiday>? Holidays { get; set; } = default!;

        protected override void OnInitialized()
        {
            Holidays = MockDataBaseService.Holidays;
        }
    }
}