using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Foodly_new.Models.DomainModels
{
    public class Restaurant
    {
        [Key, Required]
        public int RestaurantID { get; set; }
        [Required]
        public string RestaurantName { get; set; }
        [Required]
        public string RestaurantAdress { get; set; }
        public string RestaurantWeb { get; set; }
        [Required]
        public int StarCount { get; set; }
    }
}
