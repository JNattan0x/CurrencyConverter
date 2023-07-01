
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
App.DependencyInjection.DependencyRepository(builder.Services);

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapControllerRoute("default","{controller=Converter}/{action=Index}/{id?}");

app.Run();
