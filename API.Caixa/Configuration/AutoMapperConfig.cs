using API.Caixa.Domain.Entities;
using API.Caixa.Models;
using AutoMapper;
using Core.Bus.Messages;

namespace API.Caixa.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<EntryModel, Entry>().ReverseMap();
            CreateMap<AccountModel, Account>().ReverseMap();
            CreateMap<UserMessage, User>().ReverseMap();
        }
    }
}
