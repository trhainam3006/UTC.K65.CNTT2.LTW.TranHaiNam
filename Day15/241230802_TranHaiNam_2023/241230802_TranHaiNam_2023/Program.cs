using Microsoft.EntityFrameworkCore;
using _241230802_TranHaiNam_2023.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(options =>
{
    options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor(
        (value, field) => $"{field} phải là số nguyên.");
});

builder.Services.AddDbContext<QLHangHoaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("QLHangHoa")));

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
