using MagicVilla_VillaAPI.Models.Dto;

namespace MagicVilla_VillaAPI.Data
{
    public static class DataStore
    {
        public static List<VillaDto> VillaList = new List<VillaDto>
            {
                new VillaDto{Id=1,Name="Royal villa",Occupancy=2,Sqft=1000},
                new VillaDto{Id=2, Name="Pink villa",Occupancy=4,Sqft=2100}
            };
    }
}

