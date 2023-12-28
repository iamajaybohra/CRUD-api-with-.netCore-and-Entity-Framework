using MagicVillia_VillaAPI.Model.DTO;

namespace MagicVillia_VillaAPI.Data
{
    public static class VillaStore
    {
        public static List<VillaDTO> villaList = new List<VillaDTO>
            {
                new VillaDTO { Id = 1, Name ="Pool View", Occupancy = 10 , Sqrt = 20},
                new VillaDTO { Id = 2,Name ="Beach View", Occupancy = 20 , Sqrt = 30}
            };
    }
}
