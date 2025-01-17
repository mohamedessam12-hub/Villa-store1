using System.Net;
using AutoMapper;
using MagicVila_VillaAPi.Model;
using MagicVila_VillaAPi.Model.VillaDTO;
using MagicVila_VillaAPi.Repository;
using MagicVila_VillaAPi.Repository.IRepository;
using MagicVila_VillaAPi.Repository.Repository;
using Microsoft.AspNetCore.Mvc;

namespace MagicVila_VillaAPi.Controllers.V2
{
    [Route("api/v{version:apiVersion}/[Controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    public class VillaNumberController : ControllerBase
    {
        protected APIResponse _response;
        public readonly IRepositoryVillanum _repository;
        public readonly IMapper _mapper;
        public readonly IVillaRepository _villaRepository;
        public VillaNumberController(IRepositoryVillanum repository, IMapper mapper, IVillaRepository IvillaRepository)
        {
            _response = new APIResponse();
            _repository = repository;
            _mapper = mapper;
            _villaRepository = IvillaRepository;
        }

        //[MapToApiVersion("2.0")]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "version1", "version2" };
        }

    }
}
