using Foodly_new.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Foodly_new.Models.DomainModels
{
    public class Review
    {
        [Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReviewID { get; set; }
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
    }
}
