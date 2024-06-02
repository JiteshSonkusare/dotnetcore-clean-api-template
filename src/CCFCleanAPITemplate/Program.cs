using CCFClean.ApiVersioning;
using CCFCleanAPITemplate.OpenApi;
using CCFClean.Minimal.Definition;

var builder = WebApplication.CreateBuilder(args);

builder.AddEndpointDefinitions(typeof(Program).Assembly);
builder.Services.AddCCFApiVersioning();
builder.Services.AddCCFSwaggerExtenison(builder.Configuration);

var app = builder.Build();

app.MapEndpointDefinitions(opt =>
{
	opt.ApiVersions = builder.Configuration.GetSection("DefineApiVersion").Get<List<DefineApiVersion>>();
	opt.ApiPathPrefix = "api";
});
app.UseCCFSwaggerDefinition();

app.Run();