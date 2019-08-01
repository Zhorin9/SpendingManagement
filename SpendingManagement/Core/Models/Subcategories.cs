namespace SpendingManagement.Core.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Subcategories
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        public virtual Categories Category { get; set; }
    }
}