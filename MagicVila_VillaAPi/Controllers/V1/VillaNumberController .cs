using System.Net;
using AutoMapper;
using MagicVila_VillaAPi.Model;
using MagicVila_VillaAPi.Model.VillaDTO;
using MagicVila_VillaAPi.Repository;
using MagicVila_VillaAPi.Repository.IRepository;
using MagicVila_VillaAPi.Repository.Repository;
using MagicVila_VillaAPi.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MagicVila_VillaAPi.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[Controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class VillaNumberController : ControllerBase
    {
        private readonly IVillaNumberService _service;

        public VillaNumberController(IVillaNumberService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetAllVillaNumbers()
        {
            return Ok(await _service.GetAllVillaNumbersAsync());
        }

        [HttpGet("{id:int}", Name = "GetVillaNumber")]
        public async Task<ActionResult<APIResponse>> GetVillaNumber(int id)
        {
            var result = await _service.GetVillaNumberAsync(id);
            if (!result.isSuccess) return StatusCode((int)result.statusCode, result);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<APIResponse>> AddVillaNumber([FromBody] VillaNumberCreateDTO createDTO)
        {
            var result = await _service.CreateVillaNumberAsync(createDTO);
            if (!result.isSuccess) return BadRequest(result);

            var created = result.Result as VillaNumberDTO;
            return CreatedAtRoute("GetVillaNumber", new { id = created.VillaNo }, result);
        }

        [HttpPut("{id:int}", Name = "UpdateVillaNumber")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<APIResponse>> UpdateVillaNumber(int id, [FromBody] VillaNumberUpdateDTO updateDTO)
        {
            var result = await _service.UpdateVillaNumberAsync(id, updateDTO);
            if (!result.isSuccess) return StatusCode((int)result.statusCode, result);
            return NoContent();
        }

        [HttpDelete("{id:int}", Name = "RemoveVillaNumber")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<APIResponse>> RemoveVillaNumber(int id)
        {
            var result = await _service.DeleteVillaNumberAsync(id);
            if (!result.isSuccess) return StatusCode((int)result.statusCode, result);
            return Ok(result);
        }
    }
}
