namespace BESL.Application.Infrastructure.AutoMapper
{
    using System.Reflection;

    using Infrastructure.AutoMapper;
    using global::AutoMapper;

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            this.LoadStandardMappings();
            this.LoadCustomMappings();
            this.LoadConverters();
        }

        private void LoadConverters()
        {
        }

        private void LoadStandardMappings()
        {
            var mapsFrom = MapperProfileHelper.LoadStandardMappings(Assembly.GetExecutingAssembly());

            foreach (var map in mapsFrom)
            {
                CreateMap(map.Source, map.Destination).ReverseMap();
            }
        }

        private void LoadCustomMappings()
        {
            var mapsFrom = MapperProfileHelper.LoadCustomMappings(Assembly.GetExecutingAssembly());

            foreach (var map in mapsFrom)
            {
                map.CreateMappings(this);
            }
        }
    }
}
