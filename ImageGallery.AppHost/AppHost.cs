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
    .WithEnvironment("KC_PROFILE", "prod")
    .WithEnvironment("KEYCLOAK_ADMIN", "admin")
    .WithEnvironment("KEYCLOAK_ADMIN_PASSWORD", "admin")
    .WithEnvironment("KC_HOSTNAME_STRICT", "false")
    .WithEnvironment("KC_HOSTNAME_STRICT_HTTPS", "false")
    .WithEnvironment("KC_HTTP_ENABLED", "true")
    .WithEnvironment("KC_HTTP_PORT", "8080")
    .WaitFor(keycloakDb);

// SQL (from step 3)
var sql = builder.AddSqlServer("sql", port: 1433)
    .WithDataVolume()
    .WithEnvironment("ACCEPT_EULA", "Y")
    .WithEnvironment("MSSQL_SA_PASSWORD", "Aa123456")
    // .WithHealthCheck("tcp://localhost:1445")
    .WithExternalHttpEndpoints();

var db = sql.AddDatabase("ImageGallery");

// API depends on both Keycloak and DB
builder.AddProject<Projects.ImageGallery_API>("imagegallery-api")
    .WithExternalHttpEndpoints()
    .WithReference(db)
    .WithReference(keycloak)
    .WaitFor(db)
    .WaitFor(keycloak);

builder.Build().Run();