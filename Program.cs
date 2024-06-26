using Emp_Management_SF.Common;
using Emp_Management_SF.CosmosDB;
using Emp_Management_SF.Interface;
using Emp_Management_SF.ServiceFilter;
using Emp_Management_SF.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICosmosDB,CosmosDBService>();
builder.Services.AddScoped<IEmployeeInterface,EmployeeService>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddScoped <BuildEmployeeFilter>();
builder.Services.AddScoped<BuildEmployeeAdditionalFilter>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
