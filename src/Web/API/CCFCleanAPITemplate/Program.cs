using CCFCleanAPITemplate.Versioning;
using CCFCleanAPITemplate.EndpointDefinition;
using CCFCleanAPITemplate.OpenApi.EndpointDefinition;

var builder = WebApplication.CreateBuilder(args);
builder.AddEndpointDefinitions(typeof(SwaggerEndpointDefinition));
var app = builder.Build();
app.UseEndpointDefinitions(new List<DefineApiVersion> { new(1, 0, false), new(2, 0, false) });
app.Run();