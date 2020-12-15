using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Foodly_new.Models.DomainModels
{
    public class Restaurant
    {
        [Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RestaurantID { get; set; }
        [Required]
        public string RestaurantName { get; set; }
        [Required]
        public string RestaurantAdress { get; set; }
        public string RestaurantWeb { get; set; }
        [Required]
        public Double StarCount { get; set; }
        [Required]
        public string FavProduct { get; set; }
    }
}
