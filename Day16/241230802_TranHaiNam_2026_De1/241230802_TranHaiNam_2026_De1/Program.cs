using Microsoft.EntityFrameworkCore;
using _241230802_TranHaiNam_2026_De1.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews(options =>
{
    options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor(
        (value, field) => $"{field} phải là số nguyên.");
    options.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(
        field => $"{field} phải là số.");
});
builder.Services.AddDbContext<QLTaiSanContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("QLTaiSan")));

var app = builder.Build();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
