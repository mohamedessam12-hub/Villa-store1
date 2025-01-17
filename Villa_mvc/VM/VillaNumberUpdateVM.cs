using Microsoft.AspNetCore.Mvc.Rendering;
using Villa_mvc.Model.VillaDTO;

namespace Villa_mvc.VM
{
    public class VillaNumberUpdateVM
    {
        public VillaNumberUpdateDTO UpdateDTO { get; set; }
        public VillaNumberUpdateVM()
        {
            UpdateDTO = new VillaNumberUpdateDTO();
        }
        public IEnumerable<SelectListItem> ListItems { get; set; }
    }
}
