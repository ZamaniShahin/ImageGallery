using FastEndpoints.Swagger;
using ImageGallery.Infrastructure;
using ImageGallery.Infrastructure.Persistence;

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

builder.Services.AddScoped<AppDbContext>();
builder.Services.AddScoped(typeof(IAppRepository<>), typeof(AppRepository<>));
builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All;
    logging.CombineLogs = true;
});
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
app.UseFastEndpoints(c =>
    {
        // c.Versioning.Prefix = "v";
        // c.Versioning.DefaultVersion = 1;
        // c.Versioning.PrependToRoute = true;
        c.Endpoints.ShortNames = true;
        c.Endpoints.RoutePrefix = "api";
    }).UseSwaggerGen()
    ;

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();