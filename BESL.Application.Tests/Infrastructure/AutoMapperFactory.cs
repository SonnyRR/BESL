namespace BESL.Application.Tests.Infrastructure
{
    using AutoMapper;
    using BESL.Application.Infrastructure.AutoMapper;

    public class AutoMapperFactory
    {
        public static IMapper Create()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });

            return mappingConfig.CreateMapper();
        }
    }
}
