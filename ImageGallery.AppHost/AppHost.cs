using Aspire.Hosting.Keycloak;
var builder = DistributedApplication.CreateBuilder(args);


var keycloakDb = builder.AddPostgres("keycloak-db")
    .WithDataVolume()
    .AddDatabase("keycloakdb"); // database name inside Postgres


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

var sql = builder.AddSqlServer("sql", port: 1433)
    .WithDataVolume();

var db = sql.AddDatabase("ImageGallery");

var api = builder.AddProject<Projects.ImageGallery_API>("imagegallery-api")
    .WithExternalHttpEndpoints()
    .WithReference(db)
    .WithReference(keycloak)
    .WaitFor(db)
    .WaitFor(keycloak);

builder.AddNpmApp("frontend", "../../ImageGallery-UI", "dev")
    .WithHttpEndpoint(port: 5173, env: "VITE_PORT")
    .WaitFor(api);

builder.Build().Run();