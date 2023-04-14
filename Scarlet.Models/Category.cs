using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Scarlet.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        [DisplayName("Category Name")]
        public string Name { get; set; } = string.Empty;
        [Range(0, 100, ErrorMessage = "Display Order must be between 0 and 100.")]
        [DisplayName("Display Order")]
        public int DisplayOrder { get; set; }
    }
}
