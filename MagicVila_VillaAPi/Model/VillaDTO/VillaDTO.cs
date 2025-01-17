using System.ComponentModel.DataAnnotations;

namespace MagicVila_VillaAPi.Model.VillaDTO
{
    public class VillaDTO
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string Details { get; set; }
        public double Rate { get; set; }
        public int Sqft { get; set; }
        public int OcCupancy { get; set; }
        public string ImageUrl { get; set; }
        public string Amenity { get; set; }
        public DateTime DateCreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
