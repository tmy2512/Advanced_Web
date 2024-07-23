using ManagementAssistanceForBusinessWeb_OnlyRole.Context;
using ManagementAssistanceForBusinessWeb_OnlyRole.Models;
using ManagementAssistanceForBusinessWeb_OnlyRole.Repository.Project;
using ManagementAssistanceForBusinessWeb_OnlyRole.Repository.ProjectFolder;
using ManagementAssistanceForBusinessWeb_OnlyRole.Repository.UserFolder;
using ManagementAssistanceForBusinessWeb_OnlyRole.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ManagementAssistanceForBusinessWeb_OnlyRole.Models.UserViewModels;
using ManagementAssistanceForBusinessWeb_OnlyRole.Repository.TaskFolder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add services to IHttpContextAccessor
builder.Services.AddHttpContextAccessor();

// Add memory service to Session
builder.Services.AddDistributedMemoryCache();

//Register DbContext
var connectionstring = builder.Configuration.GetConnectionString("myconnectionstring");
builder.Services.AddDbContext<ModelDbContext>(item => item.UseSqlServer(connectionstring));

//Register Models service
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();

// Trong file Program.cs
builder.Services.AddScoped<IPasswordHasher<UserModel>, PasswordHasher<UserModel>>();

// Thêm dịch vụ cho session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Đặt thời gian hết hạn session
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
// add this line for authen
app.UseSession();
// thêm headers để ngăn trình duyệt lưu cache các trang đăng nhập
//app.Use(async (context, next) =>
//{
//    context.Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
//    context.Response.Headers["Pragma"] = "no-cache";
//    context.Response.Headers["Expires"] = "0";
//    await next();
//});
//app.UseMiddleware<AuthenticationMiddleware>();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
