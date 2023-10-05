using CCFCleanAPITemplate.Versioning;
using CCFCleanAPITemplate.Endpoints.V1;
using CCFCleanAPITemplate.EndpointDefinition;

var builder = WebApplication.CreateBuilder(args);
builder.AddEndpointDefinitions(typeof(UserEndpointDefinition));
var app = builder.Build();
app.UseEndpointDefinitions(new List<DefineApiVersion> { new DefineApiVersion(1, 0), new DefineApiVersion(2, 0) });
app.Run();