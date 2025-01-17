using System.Diagnostics;
using AutoMapper;
using MagicVilla_Unity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Villa_mvc.Model;
using Villa_mvc.Model.VillaDTO;
using Villa_mvc.Models;
using Villa_mvc.Service.IService;

namespace Villa_mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IVillaServics villaServics;
        private readonly IMapper mapper;

        public HomeController(IVillaServics villaServics, IMapper mapper)
        {
            this.villaServics = villaServics;
            this.mapper = mapper;
        }


        public async Task<IActionResult> Index()
        {
            List<VillaDTO> List = new();
            var res = await villaServics.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (res != null && res.isSuccess)
            {
                List = JsonConvert.DeserializeObject<List<VillaDTO>>(res.Result.ToString());
            }
            return View(List);
        }

    }
}
