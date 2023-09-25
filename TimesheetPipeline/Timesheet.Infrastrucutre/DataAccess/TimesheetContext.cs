using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Domain.Entities;
using Timesheet.Domain.Entities.Timesheets;
using Timesheet.Domain.Entities.Users;

namespace Timesheet.Infrastrucutre.DataAccess
{
    public class TimesheetContext : DbContext
    {
        public DbSet<Holiday> Holidays { get; set; }
        public DbSet<User> Users { get; set; }
        //public DbSet<TimesheetEntity> Timesheets { get; set; }

        public TimesheetContext(DbContextOptions<TimesheetContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Occupation>().HasNoKey();
            //modelBuilder.Entity<TimesheetEntity>().HasMany("Occupation");
            base.OnModelCreating(modelBuilder);
        }
    }
}