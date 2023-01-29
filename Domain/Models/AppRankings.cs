using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace MobileArts.api.Domain.Models
{
    public partial class AppRankings
    {
        public AppRankings()
        {
            AppRankingsList = new HashSet<AppRankings>();
        }

        public string App_id { get; set; }
        [Required(ErrorMessage = "id is required.")]
        [StringLength(255, ErrorMessage = "Category name must be less than {1} characters.")]

        public string id { get; set; }
        [Required(ErrorMessage = "id is required.")]
        [StringLength(255, ErrorMessage = "Category name must be less than {1} characters.")]


        public string App_name { get; set; }
        [Required(ErrorMessage = "appname is required.")]

        public string Price { get; set; }
        [Required(ErrorMessage = "Price is required.")]

        public string Icon_url { get; set; }
        [Required(ErrorMessage = "url is required.")]

        public string Publisher_name { get; set; }
      

        public string[] Genres { get; set; }

        public string[] Screenshot_urls { get; set; }

        public string Description { get; set; }

        public string All_rating { get; set; }


        public virtual ICollection<AppRankings> AppRankingsList { get; set; }
    }
}
