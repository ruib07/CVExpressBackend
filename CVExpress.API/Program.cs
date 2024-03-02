using CVExpress.API.Configurations.Documentation;
using CVExpress.API.Configurations.Persistance;
using CVExpress.API.Configurations.Security;
using CVExpress.Services;
using CVExpress.Services.Interfaces;
using CVExpress.Services.Services;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

#region Services

builder.Services.AddControllers();

builder.Services.AddCustomApiDocumentation();
builder.Services.AddCustomApiSecurity(configuration);
builder.Services.AddCustomServiceDependencyRegister(configuration);
builder.Services.AddCustomDatabaseConfiguration(configuration);

builder.Services.AddScoped<IRegisterUsersService, RegisterUsersService>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IHabilitationsService, HabilitationsService>();
builder.Services.AddScoped<IExperienceService, ExperienceService>();
builder.Services.AddScoped<IContactsService, ContactsService>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminAndUser", policy =>
        policy.RequireRole("Admin", "User"));
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy =>
        policy.RequireRole("Admin"));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder =>
    {
        builder.WithOrigins("http://localhost:4200")
                 .AllowAnyHeader()
                 .AllowAnyMethod();
    });
});

#endregion

var app = builder.Build();

app.UseCors("AllowSpecificOrigin");

app.UseCustomApiDocumentation();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
