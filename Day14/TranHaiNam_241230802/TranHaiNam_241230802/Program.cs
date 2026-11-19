using Microsoft.EntityFrameworkCore;
using TranHaiNam_241230802.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(options =>
{
    options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor(
        (value, field) => $"{field} phải là số hợp lệ.");
    options.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(
        field => $"{field} phải là số.");
});

builder.Services.AddDbContext<QLNhaSachContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("QLNhaSach")));

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

public partial class Program { }
