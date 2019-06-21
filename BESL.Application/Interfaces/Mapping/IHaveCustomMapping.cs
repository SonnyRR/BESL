namespace BESL.Application.Interfaces.Mapping
{
    using AutoMapper;

    public interface IHaveCustomMapping
    {
        void CreateMappings(Profile configuration);
    }
}
