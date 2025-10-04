using Aspire.Hosting.Keycloak;
var builder = DistributedApplication.CreateBuilder(args);


// --- PostgreSQL for Keycloak ---
var keycloakDb = builder.AddPostgres("keycloak-db")
    .WithDataVolume()
    .AddDatabase("keycloakdb"); // database name inside Postgres


// Keycloak on a stable dev port + data volume + realm import (folder path relative to AppHost)
var keycloak = builder.AddKeycloak("keycloak", 8080)
    .WithDataVolume()
    .WithRealmImport("./Realms")
    .WithReference(keycloakDb)
    .WithEnvironment("KEYCLOAK_ADMIN", "admin")
    .WithEnvironment("KEYCLOAK_ADMIN_PASSWORD", "admin");

// SQL (from step 3)
var sql = builder.AddSqlServer("sql").WithDataVolume();
var db = sql.AddDatabase("DefaultConnection");

// API depends on both Keycloak and DB
builder.AddProject<Projects.ImageGallery_API>("imagegallery-api")
    .WithExternalHttpEndpoints()
    .WithReference(db)
    .WithReference(keycloak)
    .WaitFor(db)
    .WaitFor(keycloak);

builder.Build().Run();