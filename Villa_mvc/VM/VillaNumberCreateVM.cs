using Microsoft.AspNetCore.Mvc.Rendering;
using Villa_mvc.Model.VillaDTO;

namespace Villa_mvc.VM
{
    public class VillaNumberCreateVM
    {
        public VillaNumberCreateDTO CreateDTO { get; set; }
        public VillaNumberCreateVM()
        {
            CreateDTO = new VillaNumberCreateDTO();
        }
        public IEnumerable<SelectListItem> ListItems { get; set; }
    }
}
