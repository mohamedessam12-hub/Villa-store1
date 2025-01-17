using System.ComponentModel.DataAnnotations;

namespace MagicVila_VillaAPi.Model.VillaDTO
{
    public class VillaCreateDTO
    {
      
        [Required]
        public string Name { get; set; }

        public string Details { get; set; }
        public double Rate { get; set; }
        [Required]
        public int Sqft { get; set; }
        [Required]
        public int OcCupancy { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public string Amenity { get; set; }
        
    }
}
