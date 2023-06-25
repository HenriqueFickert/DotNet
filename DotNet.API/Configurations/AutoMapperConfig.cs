using DotNet.Application.Mappings;

namespace DotNet.Presentation.Configurations
{
    public static class AutoMapperConfig
    {
        public static void AddAutoMapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(AlunoMappingProfile),
                typeof(TurmaMappingProfile));
        }
    }
}