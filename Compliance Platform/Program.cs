using Compliance_Platform.Classes;
using Compliance_Platform.Components;
using Compliance_Platform.Interfaces;
using Compliance_Platform.Model;
using Compliance_Platform.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

builder.Services.AddTransient<IEmailService, EmailService>();

//Fragment do obs³ugi kont
builder.Services.AddDefaultIdentity<IdentityUser>(options => {
    options.SignIn.RequireConfirmedAccount = false; // TBD
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;
})
.AddRoles<IdentityRole>() // Wsparcie ról
.AddEntityFrameworkStores<CompPlatformDbContext>();

builder.Services.AddScoped<IQuestionnaireRepository, QuestionnaireRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<QuestionnaireService>();
builder.Services.AddScoped<QuestionnaireState>();
builder.Services.AddScoped<RiskCalculationService>();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
