using AutoMapper;
using GnosisNet.Entities.Entities;
using GnosisNet.Service.Models;

namespace GnosisNet.API.Helpers
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Blog, BlogDto>().ReverseMap();
            });

            return mappingConfig;
        }
    }
}
