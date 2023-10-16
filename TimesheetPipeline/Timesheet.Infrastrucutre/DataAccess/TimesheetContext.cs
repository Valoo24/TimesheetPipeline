using Microsoft.EntityFrameworkCore;
using Timesheet.Domain.Entities;
using Timesheet.Domain.Entities.Timesheets;
using Timesheet.Domain.Entities.Users;

namespace Timesheet.Infrastrucutre.DataAccess
{
    public class TimesheetContext : DbContext
    {
        public DbSet<Holiday> Holidays { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<TimesheetEntity> Timesheets { get; set; }
        public DbSet<Occupation> Occupations { get; set; }

        public TimesheetContext(DbContextOptions<TimesheetContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Holiday>().HasKey(h => h.Id);

            modelBuilder.Entity<User>().HasKey(u => u.Id);

            modelBuilder.Entity<TimesheetEntity>().HasKey(t => t.Id);

            modelBuilder.Entity<Occupation>().HasKey(o => o.Id);

            modelBuilder.Entity<TimesheetEntity>().HasMany(t => t.OccupationList).WithOne(o => o.Timesheet).HasForeignKey(o => o.TimesheetId);

            modelBuilder.Entity<TimesheetEntity>().HasOne(t => t.User).WithMany(u => u.Timesheets).HasForeignKey(t => t.UserId);

            base.OnModelCreating(modelBuilder);
        }
    }
}