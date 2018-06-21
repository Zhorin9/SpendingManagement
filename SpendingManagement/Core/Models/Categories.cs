using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace SpendingManagement.Core.Models
{
    public class Categories
    {
        public byte Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        public bool isRevenue { get; set; }

        public virtual ICollection<Subcategories> Subcategories { get; set; }
    }
}