using Compliance_Platform.Classes;
using Compliance_Platform.Components;
using Compliance_Platform.Interfaces;
using Compliance_Platform.Model;
using Compliance_Platform.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// SQL konektor
var dbConnectionsString = builder.Configuration.GetConnectionString("CompPlatform");
builder.Services.AddDbContext<CompPlatformDbContext>((options) =>
{
    options.UseSqlServer(dbConnectionsString);
});

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
/*
builder.Services.AddHttpClient("LocalAPI", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["BaseAddress"] ?? builder.Configuration.GetSection("AppSettings")["BaseUrl"] ?? "https://localhost:5001/");
});*/

//Fragment do obsługi kont
builder.Services.AddIdentity<CompPlatformUser, IdentityRole>(options => {
    options.SignIn.RequireConfirmedAccount = false; // TBD
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 6;
})
.AddDefaultTokenProviders()
.AddEntityFrameworkStores<CompPlatformDbContext>();

//Placeholder pod system autoryzacyjny na przyszłość
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdministratorRole",
        policy => policy.RequireRole("Administrator"));
    options.AddPolicy("RequireAuditorRole",
        policy => policy.RequireRole("Audytor"));
    options.AddPolicy("RequireRejestratorRole",
        policy => policy.RequireRole("Rejestrator"));
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/login";
    options.LogoutPath = "/logout";
    options.AccessDeniedPath = "/access-denied";
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

//Tymczasowe rozwiązanie - security issues
builder.Services.AddControllers(options =>
{
    options.Filters.Add(new IgnoreAntiforgeryTokenAttribute());
});

builder.Services.AddScoped<IQuestionnaireRepository, QuestionnaireRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<QuestionnaireService>();
builder.Services.AddScoped<QuestionnaireState>();
builder.Services.AddScoped<RiskCalculationService>();
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddHttpContextAccessor();

builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

//await SeedData.Initialize(app.Services);

app.Run();