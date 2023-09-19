//using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace MagicVilla_VillaAPI.Models.Dto
{
    public class LoginRequestDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
