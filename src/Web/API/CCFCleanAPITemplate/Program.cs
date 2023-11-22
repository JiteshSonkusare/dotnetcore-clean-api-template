using CCFCleanAPITemplate.Versioning;
using CCFCleanAPITemplate.EndpointDefinition;
using CCFCleanAPITemplate.ApiEndpoints.V1;

var builder = WebApplication.CreateBuilder(args);
builder.AddEndpointDefinitions(typeof(UserEndpointDefinition));
var app = builder.Build();
app.UseEndpointDefinitions(new List<DefineApiVersion> { new DefineApiVersion(1, 0), new DefineApiVersion(2, 0) });
app.Run();