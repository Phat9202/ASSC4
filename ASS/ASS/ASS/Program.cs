using ASS.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<MyDb>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("ConnectDb")));
// Add session services
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(30); // thời gian hết hạn của session
    options.Cookie.HttpOnly = true; // Bảo mật session cookie bằng cách đặt HttpOnly
    options.Cookie.IsEssential = true; // Cookie sẽ không bị chặn bởi trình duyệt ngay cả khi không có sự đồng ý của người dùng
});
// Add IHttpContextAccessor service
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
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
app.UseAuthorization();
// Enable session
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
