using Timesheet.Application.Services;
using Timesheet.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<HolidayService, HolidayService>();
builder.Services.AddScoped<HolidayRepository, HolidayRepository>();

builder.Services.AddScoped<UserRepository, UserRepository>();
builder.Services.AddScoped<UserService, UserService>();

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