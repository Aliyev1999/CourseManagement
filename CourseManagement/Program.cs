using CourseManagementCore.DataAccess.SqlServer;
using CourseManagementCore.Domain.Abstraction.Interfaces;

var builder = WebApplication.CreateBuilder(args);

string connString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddScoped(typeof(IUnitOfWork), x => new SqlUnitOfWork(connString));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
