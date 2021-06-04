using AutoMapper;

namespace ProjectName.Application.Models.Mappings
{
    public interface IHaveCustomMapping
    {
        void CreateMappings(Profile configuration);
    }
}
