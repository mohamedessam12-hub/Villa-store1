using System.ComponentModel.DataAnnotations;

namespace MagicVila_VillaAPi.Model.VillaDTO
{
    public class VillaNumberUpdateDTO
    {
        [Required]
        public int VillaNo { get; set; }
        [Required]
        public int VillaId { get; set; }

        public string SpecialDetails { get; set; }
    }
}
