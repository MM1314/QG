using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using EasyEF.Common;

namespace EasyEF.Models
{
    [Table("Category")]
    public partial class Category
    {
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
