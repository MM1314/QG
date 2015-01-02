using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.ComponentModel;
using EasyEF.Common;

namespace EasyEF.Models
{
    [Table("Product")]
    public partial class Product
    {
        public int Id { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "名称不能为空")]
        public string Name { get; set; }

        public int Size { get; set; }

        [StringLength(300)]
        public string PhotoUrl { get; set; }

        public DateTime AddTime { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
