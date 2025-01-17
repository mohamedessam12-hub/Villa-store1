using System.Collections.Generic;
using AutoMapper;
using MagicVilla_Unity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Villa_mvc.Model;
using Villa_mvc.Model.VillaDTO;
using Villa_mvc.Service.IService;

namespace Villa_mvc.Controllers
{
    public class VillaController : Controller
    {
        private readonly IVillaServics villaServics;
        private readonly IMapper mapper;

        public VillaController(IVillaServics villaServics,IMapper mapper)
        {
            this.villaServics = villaServics;
            this.mapper = mapper;
        }


        public async Task<IActionResult> IndexVilla()
        {
            List<VillaDTO> List = new();
            var res = await villaServics.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (res != null && res.isSuccess)
            {
                List = JsonConvert.DeserializeObject<List<VillaDTO>>(res.Result.ToString());
            }
            return View(List);
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateVilla()
        {
            return View();  
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVilla(VillaCreateDTO villaCreate)
        {
            if (ModelState.IsValid) 
            {
                var res = await villaServics.CreateAsync<APIResponse>(villaCreate, HttpContext.Session.GetString(SD.SessionToken));
                if (res != null && res.isSuccess)
                {
                    TempData["success"] = "Villa Created Successfuly";
                    return RedirectToAction(nameof(IndexVilla));
                }
            }
            TempData["Error"] = "Error Happend";

            return View(villaCreate);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateVilla(int VillaId)
        {
            var res = await villaServics.GetAsync<APIResponse>(VillaId, HttpContext.Session.GetString(SD.SessionToken));
            if (res != null && res.isSuccess)
            {
              
                VillaDTO model = JsonConvert.DeserializeObject<VillaDTO>(Convert.ToString(res.Result));
                return View(mapper.Map<VillaUpdateDTO>(model));
            }

            return NotFound();  
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateVilla(VillaUpdateDTO updateDTO)
        {
            if (ModelState.IsValid) 
            {
                var res = await villaServics.UpdateAsync<APIResponse>(updateDTO, HttpContext.Session.GetString(SD.SessionToken));
                if (res != null && res.isSuccess)
                {
                    TempData["success"] = "Villa Updated Successfuly";

                    return RedirectToAction(nameof(IndexVilla));
                }
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }

            TempData["Error"] = "Error Happend";
            return View(updateDTO);
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteVilla(int villaid)
        {
            var res = await villaServics.GetAsync<APIResponse>(villaid, HttpContext.Session.GetString(SD.SessionToken));
            if (res != null && res.isSuccess)
            {
                VillaDTO model = JsonConvert.DeserializeObject<VillaDTO>(Convert.ToString(res.Result));
                return View(model);
            }

            return NotFound();  
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteVilla(VillaDTO delete)
        {
           
                var res = await villaServics.DeleteAsync<APIResponse>(delete.Id, HttpContext.Session.GetString(SD.SessionToken));
                if (res != null && res.isSuccess)
                {
                TempData["success"] = "Villa Deleted Successfuly";

                return RedirectToAction(nameof(IndexVilla));
                }
            TempData["Error"] = "Error Happend";

            return View(delete);
        }
    }
}
