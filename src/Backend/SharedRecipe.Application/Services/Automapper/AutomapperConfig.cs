using AutoMapper;
using SharedRecipe.Domain.Entities;
using SharedRecipe.Reporting.Requests;

namespace SharedRecipe.Application.Services.Automapper
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<UserRequestJson, User>()
                .ForMember(r => r.Password, config => config.Ignore());
        }
    }
}