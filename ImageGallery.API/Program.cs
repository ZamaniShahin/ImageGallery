using FastEndpoints.Swagger;
using ImageGallery.Core.Services.Category;
using ImageGallery.Infrastructure;
using ImageGallery.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidatorsFromAssemblyContaining<IValidationPreProcessor>(ServiceLifetime.Singleton);

builder.Services.AddFastEndpoints()
    .SwaggerDocument(o =>
    {
        o.SerializerSettings = options =>
        {
            options.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
        };
        o.DocumentSettings = d =>
        {
            d.MarkNonNullablePropsAsRequired();
            d.DocumentName = "ImageGallery.API";
            d.Title = "ImageGallery.API";
            d.Version = "v1";
        };
        o.ShortSchemaNames = true;
        o.EnableJWTBearerAuth = true;
    });

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                       ?? throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");
builder.Services.Scan(scan => scan
    .FromAssemblyOf<AddImageHandler>()
    .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)))
    .AsSelfWithInterfaces()
    .WithScopedLifetime());
//todo: move to appsettings
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost5173", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});
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
//todo: add keycloak and uncomment this codes
// builder.Services.AddAuthentication();
// builder.Services.AddAuthorization();

var app = builder.Build();
app.UseHttpLogging();
app.UseCors("AllowLocalhost5173");
// app.UseAuthentication();
// app.UseAuthorization();
app.UseFastEndpoints(c =>
    {
        c.Endpoints.ShortNames = true;
        c.Endpoints.RoutePrefix = "api";
    })
    .UseSwaggerGen();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();
