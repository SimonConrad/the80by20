using System.ComponentModel.DataAnnotations;

namespace the80by20.Masterdata.App.DTO
{
    // INFO watch out for inheritance, however there for such simple crud scenario it is ok
    // Alternative and often better way is not to inheritent and just copy properties
    public class CategoryDetailsDto : CategoryDto
    {
        [StringLength(1000)]
        public string Description { get; set; }
    }
}   
