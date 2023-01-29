using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace MobileArts.api.Domain.Models
{
    public partial class MembersTraking
    {
        public int TrakingId { get; set; }
        [Required(ErrorMessage = "Member Id is required.")]
        public int MemberId { get; set; }
        [Required(ErrorMessage = "Store Id is required.")]
        public int? StoreId { get; set; }
        [Required(ErrorMessage = "Product Id is required.")]
        public int? ProductId { get; set; }
        public int? CategoryId { get; set; }
        public int? SubcategoryId { get; set; }
        public int? ItemId { get; set; }
        public int? NumberOfViews { get; set; }
        public DateTime? LastViewedDate { get; set; }
        public DateTime? DateCreate { get; set; }
        public int? UserCreate { get; set; }
        public DateTime? DateModified { get; set; }
        public int? UserModified { get; set; }

        public virtual Member Member { get; set; }

    }
}
