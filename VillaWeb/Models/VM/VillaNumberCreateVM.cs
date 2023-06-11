using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using VillaWeb.Models.Dto.VillaNumber;

namespace VillaWeb.Models.VM
{
    public class VillaNumberCreateVM
    {
        public VillaNumberCreateVM()
        {
            VillaNumber = new VillaNumberCreateDto();
        }

        public VillaNumberCreateDto VillaNumber { get; private set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> VillaList { get; set; }

    }
}
