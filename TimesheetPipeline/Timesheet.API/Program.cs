using Timesheet.Application.Services;
using Timesheet.Domain.Interfaces;
using Timesheet.Persistence.Repositories;

//Maxime, soit gentil stp.

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IHolidayService, HolidayService>();
builder.Services.AddScoped<HolidayRepository, HolidayRepository>();

builder.Services.AddScoped<UserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<TimesheetRepository, TimesheetRepository>();
builder.Services.AddScoped<TimesheetService, TimesheetService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();