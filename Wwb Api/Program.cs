global using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.EmployeeRepository;
using EmployeeManagementSystem.IImplementation;
using EmployeeManagementSystem.IServices;
using EmployeeManagementSystem.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowReact",
                      builder =>
                      {
                          builder.WithOrigins("http://localhost:5173") // frontend URL
                                 .AllowAnyHeader()
                                 .AllowAnyMethod();
                      });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}


app.UseRouting();

app.UseCors("AllowReact"); // Apply the named policy

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
