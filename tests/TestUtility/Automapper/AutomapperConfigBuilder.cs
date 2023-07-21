using AutoMapper;
using SharedRecipe.Application.Services.Automapper;

namespace TestUtility.Automapper
{
    public class AutomapperConfigBuilder
    {
        public static IMapper CreateInstance()
        {
            MapperConfiguration automapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutomapperConfig>();
            });

            return automapperConfig.CreateMapper();
        }
    }
}
