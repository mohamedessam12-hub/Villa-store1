using System.ComponentModel.DataAnnotations;

namespace MagicVila_VillaAPi.Model.VillaDTO
{
    public class VillaNumberCreateDTO
    {
        [Required]
        public int VillaNo { get; set; }
        public string SpecialDetails { get; set; }
        [Required]
        public int VillaId { get; set; }
    }
}
