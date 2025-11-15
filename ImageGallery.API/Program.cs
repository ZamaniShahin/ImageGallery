using System.Security.Claims;
using FastEndpoints;
using FastEndpoints.Swagger;
using ImageGallery.Core.Services.Category;
using ImageGallery.Infrastructure;
using ImageGallery.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidatorsFromAssemblyContaining<IValidationPreProcessor>(ServiceLifetime.Singleton);
builder.AddServiceDefaults();
builder.WebHost.UseUrls("http://0.0.0.0:7113");
//todo: move to appsettings
var keycloakRealm = "ImageGallery";
var keycloakBaseUrl = "http://localhost:8080";
var keycloakAuthority = $"{keycloakBaseUrl}/realms/{keycloakRealm}";

builder.Services.AddCors(options =>
{
    options.AddPolicy("ImageGalleryCors", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services
    .AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = keycloakAuthority;
        options.MetadataAddress = $"{keycloakAuthority}/.well-known/openid-configuration";
        options.RequireHttpsMetadata = false;

        
        options.TokenValidationParameters = new()
        {
            ValidateAudience = false,
            ValidateIssuer = true,
            RoleClaimType = ClaimTypes.Role
        };

        options.Events = new JwtBearerEvents
        {
            OnTokenValidated = context =>
            {
                var identity = context.Principal!.Identity as ClaimsIdentity;

                // realm roles
                var realmRoles = context.Principal.FindAll("realm_access.roles");
                foreach (var realmRole in realmRoles)
                    identity!.AddClaim(new Claim(ClaimTypes.Role, realmRole.Value));

                // client roles (imagegallery-frontend)
                var resourceAccess = context.Principal.FindFirst("resource_access");
                if (resourceAccess != null)
                {
                    using var doc = System.Text.Json.JsonDocument.Parse(resourceAccess.Value);

                    if (doc.RootElement.TryGetProperty("imagegallery-frontend", out var frontendClient))
                    {
                        if (frontendClient.TryGetProperty("roles", out var roles))
                        {
                            foreach (var r in roles.EnumerateArray())
                            {
                                var roleName = r.GetString();
                                if (!string.IsNullOrWhiteSpace(roleName))
                                    identity!.AddClaim(new Claim(ClaimTypes.Role, roleName));
                            }
                        }
                    }
                }

                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                       ?? throw new InvalidOperationException(
                           "Connection string 'DefaultConnection' is not configured.");

builder.Services.Scan(scan => scan
    .FromAssemblyOf<AddImageHandler>()
    .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)))
    .AsSelfWithInterfaces()
    .WithScopedLifetime());

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped(typeof(IAppRepository<>), typeof(AppRepository<>));
builder.Services.AddCoreServices();

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All;
    logging.CombineLogs = true;
});

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();


app.UseRouting();
app.UseCors("ImageGalleryCors");
app.UseAuthentication();
app.UseAuthorization();
var apiGroup = app.MapGroup("/api");
apiGroup.MapFastEndpoints();

app.UseHttpLogging();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.Run();
