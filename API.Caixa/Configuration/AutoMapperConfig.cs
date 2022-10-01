using API.Caixa.Domain.Entities;
using API.Caixa.Models;
using AutoMapper;

namespace API.Caixa.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<EntryModel, Entry>().ReverseMap();
        }
    }
}
