using System.ComponentModel.DataAnnotations;

namespace Villa_mvc.Model.VillaDTO
{
    public class VillaNumberDTO
    {
        [Required]
        public int VillaNo { get; set; }
        public string SpecialDetails { get; set; }
        [Required]
        public int VillaId { get; set; }
        public VillaDTO villa { get; set; }

    }
}
