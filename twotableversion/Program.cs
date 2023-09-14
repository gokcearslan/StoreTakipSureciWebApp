using twotableversion;
using twotableversion.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();

builder.Services.AddSingleton<DbforlastversionContext>();
var app = builder.Build();


//builder.Services.AddRazorPages();

//builder.Services.AddSignalR();
//app.MapHub<UygulamalarHub>("/uygulamalarHub");


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

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapHub<CrudHub>("/crudhub");
//}
//);

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
