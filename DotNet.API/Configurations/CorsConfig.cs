namespace DotNet.Presentation.Configurations
{
    public static class CorsConfig
    {
        public static void AddCorsConfiguration(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("Development", builder =>
                {
                    builder.AllowAnyMethod()
                           .AllowAnyHeader()
                           .SetIsOriginAllowed(origin => true)
                           .AllowCredentials();
                });
            });
        }
    }
}