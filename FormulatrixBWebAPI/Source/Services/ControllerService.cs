using System.Text.Json.Serialization;

namespace FormulatrixBWebAPI.Services
{
	public static class ControllerContainer
	{
		public static IServiceCollection ControllerServices(this IServiceCollection services, IConfiguration configuration)
		{

			services.AddControllers()
				.AddJsonOptions(options =>
				{
					options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
				});
				
			return services;
		}
	}
}

