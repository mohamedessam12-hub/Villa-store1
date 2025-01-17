using AutoMapper;
using MagicVilla_Unity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Villa_mvc.Model;
using Villa_mvc.Model.VillaDTO;
using Villa_mvc.Service;
using Villa_mvc.Service.IService;
using Villa_mvc.VM;

namespace Villa_mvc.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly IVillaNumberServics numberServics;
        private readonly IVillaServics villaServics;
        private readonly IMapper mapper;

        public VillaNumberController(IVillaNumberServics numberServics, IMapper mapper, IVillaServics villaServics)
        {
            this.numberServics = numberServics;
            this.mapper = mapper;
            this.villaServics = villaServics;
        }

        public async Task<IActionResult> IndexVillaNumber()
        {
            List<VillaNumberDTO> List = new();
            var res = await numberServics.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (res != null && res.isSuccess)
            {
                List = JsonConvert.DeserializeObject<List<VillaNumberDTO>>(res.Result.ToString());
            }
            return View(List);

        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateVillaNumber()
        {
            VillaNumberCreateVM villaNumber = new();
            var res = await villaServics.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (res != null && res.isSuccess)
            {
                villaNumber.ListItems = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(res.Result)).Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
            }
            return View(villaNumber);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVillaNumber(VillaNumberCreateVM villaNumberCreate)
        {
            if (ModelState.IsValid)
            {
                var res = await numberServics.CreateAsync<APIResponse>(villaNumberCreate.CreateDTO, HttpContext.Session.GetString(SD.SessionToken));

                if (res != null && res.isSuccess)
                {
                    // إذا كانت العملية ناجحة، عد إلى القائمة
                    return RedirectToAction(nameof(IndexVillaNumber));
                }

                // إذا كان هناك خطأ، قم بإضافة رسائل الخطأ إلى ModelState
                if (res?.ErorMassege != null)
                {
                    foreach (var error in res.ErorMassege)
                    {
                        ModelState.AddModelError(string.Empty, error);
                    }
                }
            }

            // إعادة تعبئة القائمة المنسدلة إذا كانت البيانات غير صالحة
            var villaRes = await villaServics.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (villaRes != null && villaRes.isSuccess)
            {
                villaNumberCreate.ListItems = JsonConvert
                    .DeserializeObject<List<VillaDTO>>(Convert.ToString(villaRes.Result))
                    .Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
            }

            return View(villaNumberCreate); // ابقَ في الصفحة مع عرض الأخطاء
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateVillaNumber(int VillaNo)
        {
            VillaNumberUpdateVM villaNumberVM = new VillaNumberUpdateVM();
            var res = await numberServics.GetAsync<APIResponse>(VillaNo, HttpContext.Session.GetString(SD.SessionToken));
            if (res != null && res.isSuccess)
            {
                VillaNumberDTO Mdoel = JsonConvert.DeserializeObject<VillaNumberDTO>(Convert.ToString(res.Result));
                villaNumberVM.UpdateDTO = mapper.Map<VillaNumberUpdateDTO>(Mdoel);
            }
            res = await villaServics.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (res != null && res.isSuccess)
            {
                villaNumberVM.ListItems = JsonConvert.DeserializeObject<List
                    <VillaDTO>>(Convert.ToString(res.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()

                    });
                return View(villaNumberVM);
            }
            return NotFound();
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateVillaNumber(VillaNumberUpdateVM Model)
        {
            if (ModelState.IsValid)
            {
                var res = await numberServics.UpdateAsync<APIResponse>(Model.UpdateDTO, HttpContext.Session.GetString(SD.SessionToken));

                if (res != null && res.isSuccess) 
                {
                    // إذا كانت العملية ناجحة، عد إلى القائمة
                    return RedirectToAction(nameof(IndexVillaNumber));
                }

                // إذا كان هناك خطأ، قم بإضافة رسائل الخطأ إلى ModelState
                if (res?.ErorMassege != null)
                {
                    foreach (var error in res.ErorMassege)
                    {
                        ModelState.AddModelError(string.Empty, error);
                    }
                }
            }

            // إعادة تعبئة القائمة المنسدلة إذا كانت البيانات غير صالحة
            var villaRes = await villaServics.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (villaRes != null && villaRes.isSuccess)
            {
                    Model.ListItems = JsonConvert
                    .DeserializeObject<List<VillaDTO>>(Convert.ToString(villaRes.Result))
                    .Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
            }

            return View(Model); // ابقَ في الصفحة مع عرض الأخطاء
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteVillaNumber(int VillaNo)
        {
            VillaNumberDeleteVM villaNumberVM = new VillaNumberDeleteVM();
            var res = await numberServics.GetAsync<APIResponse>(VillaNo, HttpContext.Session.GetString(SD.SessionToken));
            if (res != null && res.isSuccess)
            {
                VillaNumberDTO Mdoel = JsonConvert.DeserializeObject<VillaNumberDTO>(Convert.ToString(res.Result));
                villaNumberVM.DeleteDTO = Mdoel;
            }
            res = await villaServics.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (res != null && res.isSuccess)
            {
                villaNumberVM.ListItems = JsonConvert.DeserializeObject<List
                    <VillaDTO>>(Convert.ToString(res.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()

                    });
                return View(villaNumberVM);
            }
            return NotFound();
        }
         [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteVillaNumber(VillaNumberDeleteVM Model)
        {
            var res = await numberServics.DeleteAsync<APIResponse>(Model.DeleteDTO.VillaNo, HttpContext.Session.GetString(SD.SessionToken));
            if(res != null && res.isSuccess)
            {
                return RedirectToAction(nameof(IndexVillaNumber));
            }
            return View(Model); 
        }

    }
}
