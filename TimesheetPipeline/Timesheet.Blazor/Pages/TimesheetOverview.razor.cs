using Timesheet.Blazor.FakeDB;
using Timesheet.Domain.Entities.Timesheets;

namespace Timesheet.Blazor.Pages
{
    public partial class TimesheetOverview
    {
        public List<TimesheetEntity> Timesheets { get; set; } = default!;

        protected override void OnInitialized()
        {
            Timesheets = MockDataBaseService.Timesheets;
        }
    }
}