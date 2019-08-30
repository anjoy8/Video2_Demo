

using AutoMapper;

namespace Video2_Demo.Configs
{
    public class AutoMapperConfigs  
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new GoodsProfile());
            });
        }
    }
}
