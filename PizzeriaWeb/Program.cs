using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Identity;
using Pizzeria.API.Extensions;
using Pizzeria.Core.Extensions;
using Pizzeria.Domain.Entities;
using Pizzeria.Infrastructure;
using Pizzeria.Infrastructure.Extensions;
using Pizzeria.Infrastructure.Identities;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//KeyVault
var keyVaultUri = new Uri("https://keyvaultpizzeriatomasos.vault.azure.net/");
builder.Configuration.AddAzureKeyVault(keyVaultUri, new DefaultAzureCredential());


//Services
builder.Services.AddExtendedContext(builder.Configuration);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;


    });

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationUserContext>()
    .AddDefaultTokenProviders();

builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddSwagger();
builder.Services.AddCoreDI();
builder.Services.AddInfrastructureDI();


var app = builder.Build();




app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "PizzeriaDB API v1");
    c.RoutePrefix = string.Empty;
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

// Skapa Admin vid uppstart
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    await SeedAdminUser(userManager, roleManager);
}

app.Run();

async Task SeedAdminUser(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
{
    string adminEmail = "admin@pizzeria.com";
    string adminPassword = "Admin123!";

    if (!await roleManager.RoleExistsAsync("Admin"))
        await roleManager.CreateAsync(new IdentityRole("Admin"));

    if (!await roleManager.RoleExistsAsync("RegularUser"))
        await roleManager.CreateAsync(new IdentityRole("RegularUser"));

    if (!await roleManager.RoleExistsAsync("PremiumUser"))
        await roleManager.CreateAsync(new IdentityRole("PremiumUser"));

    var adminUser = await userManager.FindByEmailAsync(adminEmail);

    if (adminUser == null)
    {
        var user = new ApplicationUser
        {
            UserName = "admin",
            Email = adminEmail,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(user, adminPassword);

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, "Admin");
        }
    }
}
//slutar här

//app.Run();
