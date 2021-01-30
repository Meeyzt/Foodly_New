using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Foodly_new.Models.DomainModels
{
    public class Restaurant
    {
        [Key, Required]
        public string RestaurantID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Adress { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Tel { get; set; }
        public string Web { get; set; }
        [Required]
        public int StarCount { get; set; }
        public bool IsAccepted { get; set; }
        public string CreatedByID { get; set; }
    }
}
