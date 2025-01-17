using AutoMapper;
using Villa_mvc.Model.VillaDTO;

namespace Villa_mvc
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<VillaDTO,VillaCreateDTO >().ReverseMap();
            CreateMap<VillaDTO,VillaUpdateDTO >().ReverseMap();

            CreateMap<VillaNumberDTO,VillaNumberCreateDTO>().ReverseMap();
            CreateMap<VillaNumberDTO, VillaNumberUpdateDTO>().ReverseMap();
        }
    }
}
