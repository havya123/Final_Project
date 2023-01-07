var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddSession(options =>
{
    options.Cookie.Name = "Hutech.Session";
    options.IdleTimeout = TimeSpan.FromSeconds(10);
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "profile",
    pattern: "{controller=profile}/{action= Index}");

app.MapControllerRoute(
    name: "profile/order",
    pattern: "{controller=profile}/{action= Order}");

app.MapControllerRoute(
    name: "profile/password",
    pattern: "{controller=profile}/{action= ChangePassword}");

app.MapControllerRoute(
    name: "profile/main_profile",
    pattern: "{controller=profile}/{action= MainProfile}");
app.MapControllerRoute(
    name: "login",
    pattern: "{controller=Login}/{action=Index}");

app.UseSession();
app.Run();
