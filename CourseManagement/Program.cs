using CourseManagement.Enums;
using CourseManagement.Infrastructure;
using CourseManagement.Interfaces;
using CourseManagement.Services;
using CourseManagementEFCore;
using CourseManagementEFCore.DataAccess.SqlServer;
using CourseManagementEFCore.Domain.Abstractions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .Enrich.FromLogContext()
  .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);


builder.Services.AddControllersWithViews();

builder.Services.AddMvc(options => options.EnableEndpointRouting = false).AddXmlSerializerFormatters();
builder.Services.AddHttpContextAccessor();
builder.Services.AddMemoryCache();

builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

builder.Services.AddScoped<ICacheService, CacheServis>();

builder.Services.AddDbContext<AppDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("CourseManagementDB")));

builder.Services.AddScoped<IUnitOfWork, SqlUnitOfWork>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromDays(7);
    options.LoginPath = "/Login/Index";
    options.AccessDeniedPath = "/AccessDenied/Index";
    options.ReturnUrlParameter = "returnUrl";
});

builder.Services.AddAuthorization(config =>
{
    config.AddPolicy($"{Policy.Admin}", policyBuilder => policyBuilder.RequireClaim(ClaimTypes.Role, $"{(int)UserType.Admin}"));
    config.AddPolicy($"{Policy.User}", policyBuilder => policyBuilder.RequireClaim(ClaimTypes.Role, $"{(int)UserType.User}"));
});

builder.Services.AddScoped<IAuthorizationHandler, PermissionPolicyHandler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
}

app.UseStatusCodePagesWithReExecute("/Error/{0}");

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
