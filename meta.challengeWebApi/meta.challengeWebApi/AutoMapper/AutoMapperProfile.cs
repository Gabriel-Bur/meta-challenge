using AutoMapper;
using meta.challengeWebApi.Models;
using meta.challengeWebApi.ViewModel;

namespace meta.challengeWebApi.AutoMapper
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ContatoCreate, Contato>();
            CreateMap<ContatoPut, Contato>();
        }
    }
}
