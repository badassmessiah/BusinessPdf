

var builder = DistributedApplication.CreateBuilder(args);


var apiService = builder.AddProject<Projects.BusinessPdf_ApiService>("apiservice")
    .WithEndpoint(name: "swagger", port: 5010, scheme: "https"); // Add a named endpoint for Swagger


builder.AddProject<Projects.BusinessPdf_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
