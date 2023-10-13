using Timesheet.Blazor.FakeDB;
using Timesheet.Domain.Entities.Users;

namespace Timesheet.Blazor.Pages
{
    public partial class UserOverview
    {
        public List<User>? Users { get; set; } = default!;

        protected override void OnInitialized()
        {
            Users = MockDataBaseService.Users;
        }
    }
}