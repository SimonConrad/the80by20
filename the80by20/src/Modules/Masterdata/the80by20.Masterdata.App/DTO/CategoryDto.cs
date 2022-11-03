using System.ComponentModel.DataAnnotations;

namespace the80by20.Modules.Masterdata.App.DTO
{
    public class CategoryDto
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }
    }
}
