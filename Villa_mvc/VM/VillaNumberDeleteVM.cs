using Microsoft.AspNetCore.Mvc.Rendering;
using Villa_mvc.Model.VillaDTO;

namespace Villa_mvc.VM
{
    public class VillaNumberDeleteVM
    {
        public VillaNumberDTO DeleteDTO { get; set; }
        public VillaNumberDeleteVM()
        {
            DeleteDTO = new VillaNumberDTO();
        }
        public IEnumerable<SelectListItem> ListItems { get; set; }
    }
}
