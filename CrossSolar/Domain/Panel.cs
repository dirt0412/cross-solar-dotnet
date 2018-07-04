using System.ComponentModel.DataAnnotations;

namespace CrossSolar.Domain
{
    public class Panel
    {
        public int Id { get; set; }

        [Range(-90, 90)]
        [RegularExpression(@"^\d+(\.\d{6})$")]
        [Required] public double Latitude { get; set; }

        [Required]
        [RegularExpression(@"^\d+(\.\d{6})$")]
        [Range(-180, 180)]
        public double Longitude { get; set; }

        [RegularExpression(@"\d{16}")]
        [Required] public string Serial { get; set; }

        public string Brand { get; set; }
    }
}