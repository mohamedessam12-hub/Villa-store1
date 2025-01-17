using AutoMapper;
using MagicVila_VillaAPi.Model;
using MagicVila_VillaAPi.Model.VillaDTO;

namespace MagicVila_VillaAPi
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Villa, VillaDTO>().ReverseMap();
            CreateMap<Villa, VillaCreateDTO>().ReverseMap();
            CreateMap<Villa, VillaUpdateDTO>().ReverseMap();
            CreateMap<ApplicationUser, UserDTO>()
              .ForMember(d => d.Token, o => o.Ignore()); 
            CreateMap<UserDTO, ApplicationUser>();
            CreateMap<VillaNumber, VillaNumberDTO>().ReverseMap();
            CreateMap<VillaNumber, VillaNumberCreateDTO>().ReverseMap();
            CreateMap<VillaNumber, VillaNumberUpdateDTO>().ReverseMap();
        }
    }
}
