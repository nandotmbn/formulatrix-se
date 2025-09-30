

using FormulatrixB;
using FormulatrixBWebAPI.Services;
using Microsoft.OpenApi.Models;
using WebAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swagger =>
{
	swagger.SwaggerDoc("v1", new OpenApiInfo
	{
		Version = "v1",
		Title = "Formulatrix API : Create Read Update and Delete",
		Description = "Auth by JWT Bearer Token"
	});

	swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
	{
		Name = "Authorization",
		Type = SecuritySchemeType.ApiKey,
		Scheme = "Bearer",
		BearerFormat = "JSON Web Token",
		Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
		In = ParameterLocation.Header
	});

	swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
		{
				{
						new OpenApiSecurityScheme
						{
								Reference = new OpenApiReference
										{
												Type = ReferenceType.SecurityScheme,
												Id = "Bearer",
										}
						}, Array.Empty<string>()
				}
		});
});

builder.Services.AddCors(options =>
	{
		options.AddDefaultPolicy(builder =>
		{
			builder
				.AllowAnyOrigin()
				.AllowAnyHeader()
				.AllowAnyMethod();
		});
	});

builder.Services.ControllerServices(builder.Configuration);
builder.Services.FormulatrixBServices(builder.Configuration);
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new EnumConverterFactory()));

var app = builder.Build();

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
	var openapi = "/swagger/v1/swagger.json";
	app.UseSwagger(ops =>
	{
		ops.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi3_0;
	});
	app.UseSwaggerUI(c =>
	 {
		 c.SwaggerEndpoint(openapi, "Formulatrix API");
	 });
	app.UseReDoc(options =>
	{
		options.DocumentTitle = "Formulatrix API";
		options.SpecUrl(openapi);
	});
}

app.MapControllers();

app.Run();