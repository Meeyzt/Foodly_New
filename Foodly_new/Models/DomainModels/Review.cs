using System;
using System.ComponentModel.DataAnnotations;

namespace Foodly_new.Models.DomainModels
{
    public class Review
    {
        [Key, Required]
        public string ReviewID { get; set; }
        [Required]
        public string Header { get; set; }
        [Required]
        public string Blog { get; set; }
        [Required]
        public string RestaurantName { get; set; }
        [Required]
        public double Star { get; set; }
        [Required]
        public DateTime PublishDate { get; set; }
        [Required]
        public string BannerImage { get; set; }
        [Required]
        public string ShortCast { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public bool Publish { get; set; }
        [Required]
        public string UserID { get; set; }
        public bool IsDeleted { get; set; }
    }
}
