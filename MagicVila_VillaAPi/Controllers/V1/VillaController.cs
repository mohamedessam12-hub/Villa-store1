
using System.Net;
using AutoMapper;
using Azure;
using MagicVila_VillaAPi.Model;
using MagicVila_VillaAPi.Model.VillaDTO;
using MagicVila_VillaAPi.Repository;
using MagicVila_VillaAPi.Repository.Repository;
using MagicVila_VillaAPi.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace MagicVila_VillaAPi.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[Controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class VillaController : ControllerBase
    {
        private readonly IVillaService _villaService;
        private object _response;
        private IVillaRepository _repository;
        private IMapper _AutoMapper;

        public VillaController(IVillaRepository repository, IMapper autoMapper, IVillaService villaService)
        {
            _response = new();
            _repository = repository;
            _AutoMapper = autoMapper;
            _villaService = villaService;
        }

        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetAllVillas([FromQuery] int Rate, [FromQuery] string? Search)
            => Ok(await _villaService.GetAllVillas(Rate, Search));

        [HttpGet("{id:int}", Name = "Get Villa")]
        public async Task<ActionResult<APIResponse>> GetVilla(int id)
            => Ok(await _villaService.GetVilla(id));

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<APIResponse>> AddVilla([FromBody] VillaCreateDTO createDTO)
        {
            var response = await _villaService.AddVilla(createDTO);
            if (response.statusCode == HttpStatusCode.Created)
                return CreatedAtRoute("GetVilla", new { id = ((VillaDTO)response.Result).Id }, response);
            return BadRequest(response);
        }

        [HttpDelete("{id:int}", Name = "Remove Villa")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<APIResponse>> RemoveVilla(int id)
            => Ok(await _villaService.RemoveVilla(id));

        [HttpPut("{id:int}", Name = "Update Villa")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<APIResponse>> UpdateVilla(int id, [FromBody] VillaUpdateDTO updateDTO)
            => Ok(await _villaService.UpdateVilla(id, updateDTO));

        [HttpPatch("{id:int}", Name = "Update Partial Villa")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<APIResponse>> UpdatePartialVilla(int id, [FromBody] JsonPatchDocument<VillaUpdateDTO> patchDoc)
            => Ok(await _villaService.UpdatePartialVilla(id, patchDoc, ModelState));

    }
}
