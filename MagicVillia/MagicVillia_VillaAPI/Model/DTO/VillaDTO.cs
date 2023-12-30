using System.ComponentModel.DataAnnotations;

namespace MagicVillia_VillaAPI.Model.DTO
{
    public class VillaDTO
    {
        public int Id { get; set; }
        //Data Annotation is used to give constrain to the model attribute/variable
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public string Details { get; set; }
        public double Rate { get; set; }
        public int Sqft { get; set; }
        public int Occupancy { get; set; }
        public string ImageUrl { get; set; }
        public string Amenity { get; set; }

    }
}
