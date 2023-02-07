using Microsoft.EntityFrameworkCore;
using Notissimus.Core.Extensions;
using Notissimus.DataAccess.Extensions;

var builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;
var configuration = builder.Configuration;

services.AddCoreServices(o =>
    o.XmlDocumentUrl = configuration.GetSection("XmlDocumentUrl").Value!);

services.AddDatabaseContext(o => o
    .UseSqlServer(configuration.GetConnectionString("DbConnection")));

services.AddControllersWithViews();

WebApplication app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

await using var scope = app.Services.CreateAsyncScope();
await scope.ServiceProvider.UseDatabaseContext();

app.Run();
