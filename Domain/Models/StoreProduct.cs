using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace BastaOnline.api.Domain.Models
{
    public partial class StoreProduct
    {
        [Required(ErrorMessage = "Store Id is required.")]
        public int StoreId { get; set; }
        [Required(ErrorMessage = "Product Id is required.")]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Store type is required.")]
        public int TypeId { get; set; }
        public int? UserCreate { get; set; }
        public DateTime? DateCreate { get; set; }
        public int? UserModified { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? Active { get; set; }

        public virtual Product Product { get; set; }
        public virtual Store Store { get; set; }
    }
}
