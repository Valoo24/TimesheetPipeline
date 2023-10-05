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

            //modelBuilder.Entity<TimesheetEntity>().HasOne(t => t.User).WithMany().HasForeignKey(t => t.UserId);

            //modelBuilder.Entity<User>().HasMany(u => u.Timesheets).WithOne(t => t.User).HasForeignKey(t => t.UserId);

            modelBuilder.Entity<TimesheetEntity>().HasMany(t => t.OccupationList).WithOne(o => o.Timesheet).HasForeignKey(o => o.TimesheetId);

            base.OnModelCreating(modelBuilder);
        }
    }
}