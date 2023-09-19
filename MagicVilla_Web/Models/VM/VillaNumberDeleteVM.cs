using MagicVilla_Web.Models.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MagicVilla_Web.Models.VM
{
    public class VillaNumberDeleteVM
    {

        public VillaNumberDto VillaNumber{ get; set; }
        public VillaNumberDeleteVM()
        {
            VillaNumber= new VillaNumberDto();
        }
        [ValidateNever]
        public IEnumerable<SelectListItem> VillaList { get; set; }
    }
}
