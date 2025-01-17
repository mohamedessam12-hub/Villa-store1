
using System.Net;
using AutoMapper;
using MagicVila_VillaAPi.Model;
using MagicVila_VillaAPi.Model.VillaDTO;
using MagicVila_VillaAPi.Repository.Repository;
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
        protected APIResponse _response;
        private readonly IVillaRepository _repository;

        private readonly IMapper _AutoMapper;

        public VillaController(IVillaRepository repository, IMapper autoMapper)
        {
            _response = new();
            _repository = repository;
            _AutoMapper = autoMapper;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetAllVillas([FromQuery(Name ="ratefilter")]int Rate,[FromQuery]string? Search)
        {
            try
            {
                IEnumerable<Villa> villaList;
                if (Rate > 0)
                {
                    villaList = await _repository.GetAllAsync(e =>e.Rate == Rate);
                }
                villaList= await _repository.GetAllAsync();
                if (!string.IsNullOrEmpty(Search))
                {
                    villaList = villaList.Where(s => s.Name.ToLower().Contains(Search));
                }
                _response.Result = _AutoMapper.Map<IEnumerable<VillaDTO>>(villaList);
                _response.statusCode = HttpStatusCode.OK;
                _response.isSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.ErorMassege = new List<string>() { ex.ToString() };

            }
            return _response;
        }



        [HttpGet("{id:int}", Name = "Get Villa")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetVilla(int id)
        {
            try
            {

                if (id == 0)
                {
                    return BadRequest();
                }
                var villa = await _repository.GetAsync(v => v.Id == id);
                if (villa == null)
                {
                    return NotFound();
                }
                _response.Result = _AutoMapper.Map<VillaDTO>(villa);
                _response.isSuccess = true;
                _response.statusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.ErorMassege = new List<string>() { ex.ToString() };

            }
            return _response;
        }
        [HttpPost]
        [Authorize(Roles ="admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> AddVilla([FromBody] VillaCreateDTO createDTO)
        {
            try
            {
                if (await _repository.GetAsync(v => v.Name.ToLower() == createDTO.Name.ToLower()) != null)
                {
                    ModelState.AddModelError("", "Villa already exists");
                    return BadRequest(ModelState);
                }
                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                Villa Villa = _AutoMapper.Map<Villa>(createDTO);
                await _repository.CreateAsync(Villa);

                _response.Result = _AutoMapper.Map<VillaDTO>(Villa);
                _response.statusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetVilla", new { id = Villa.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.ErorMassege = new List<string>() { ex.ToString() };

            }
            return _response;

        }

        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id:int}", Name = "Remove Villa")]
        public async Task<ActionResult<APIResponse>> RemoveVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var villa = await _repository.GetAsync(v => v.Id == id);
                if (villa == null)
                {
                    return NotFound();
                }
                await _repository.RemoveAsync(villa);

                _response.statusCode = HttpStatusCode.OK;
                _response.isSuccess = true;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.ErorMassege = new List<string>() { ex.ToString() };

            }
            return _response;
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "admin")]
        [HttpPut("{id:int}", Name = "Update Villa")]
        public async Task<ActionResult<APIResponse>> UpdateVilla(int id, [FromBody] VillaUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.Id)
                {
                    return BadRequest();
                }

                Villa model = _AutoMapper.Map<Villa>(updateDTO);

                await _repository.UpdateAsync(model);
                _response.statusCode = HttpStatusCode.NoContent;
                _response.isSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.ErorMassege = new List<string>() { ex.ToString() };

            }
            return _response;
        }


        [HttpPatch("{id:int}", Name = "Update Partial Villa")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize(Roles = "admin")]
         [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdatePartialVilla(int id, [FromBody] JsonPatchDocument<VillaUpdateDTO> patchDoc)
        {
            if (id == null || patchDoc == null)
            {
                return BadRequest();
            }
            var villa = await _repository.GetAsync(v => v.Id == id, false);
            VillaUpdateDTO villaDTO = _AutoMapper.Map<VillaUpdateDTO>(villa);

            if (villa == null)
            {
                return NotFound();
            }
            patchDoc.ApplyTo(villaDTO, ModelState);

            Villa Model = _AutoMapper.Map<Villa>(villaDTO);

            await _repository.UpdateAsync(Model);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }
    }
}
