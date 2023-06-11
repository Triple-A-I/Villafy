using System.ComponentModel.DataAnnotations;

namespace VillaWeb.Models.Dto.VillaNumber
{
    public class VillaNumberUpdateDto
    {
        [Required]
        public int VillaNo { get; set; }
        public string SpecialDetails { get; set; }
        [Required]

        public int VillaId { get; set; }

    }
}
