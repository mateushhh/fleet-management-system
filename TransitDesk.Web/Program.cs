using Microsoft.AspNetCore.Localization;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// --- 1. Rejestracja us³ug ---
builder.Services.AddControllersWithViews();

// Rejestracja w³asnego ograniczenia trasy pod kluczem "fleetSideNo"
builder.Services.Configure<RouteOptions>(options =>
{
    options.ConstraintMap.Add("fleetSideNo", typeof(TransitDesk.Web.Routing.Constraints.FleetSideNumberConstraint));
});

builder.Services.AddControllersWithViews()
    .AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[] { new CultureInfo("pl-PL"), new CultureInfo("en-US") };
    options.DefaultRequestCulture = new RequestCulture("pl-PL");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

// Rejestracja pamiêci i sesji
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(15);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpContextAccessor();

// Konfiguracja polityki ciasteczek
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => false;
    options.MinimumSameSitePolicy = SameSiteMode.Lax;
});

var app = builder.Build();

// --- 2. Potok Middleware ---
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseStatusCodePagesWithReExecute("/Errors/Status", "?code={0}");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRequestLocalization();
app.UseRouting();

app.UseSession();
app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();

// --- 3. Trasy (Routing) ---

app.MapControllerRoute(
    name: "depot_fleet",
    pattern: "Depot/{depotName}/{tractionType?}",
    defaults: new { controller = "Vehicles", action = "DepotFleetList" });

// Domyœlna trasa konwencjonalna
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

public partial class Program { }