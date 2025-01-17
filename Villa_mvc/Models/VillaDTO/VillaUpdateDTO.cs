using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Villa_mvc.Model.VillaDTO
{
    public class VillaUpdateDTO
    {
        [Required]
        public int Id { get; set; }
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
        public string Amenity { get; set; }
       
    }
}
