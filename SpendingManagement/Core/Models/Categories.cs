namespace SpendingManagement.Core.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Categories
    {
        public byte Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        public bool IsRevenue { get; set; }

        public virtual ICollection<Subcategories> Subcategories { get; set; }
    }
}