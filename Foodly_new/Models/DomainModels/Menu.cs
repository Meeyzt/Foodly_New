using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Foodly_new.Models.DomainModels
{
    public class Menu
    {
        [Required]
        public string MenuID { get; set; }
        [Required]
        public string MenuHeader { get; set; }
        [Required]
        public string RestaurantName { get; set; }
        [Required]
        public List<string> Photos { get; set; }
        [Required]
        public DateTime PublishDate { get; set; }
        [Required]
        public string Date { get; set; }
        [Required]
        public string UserID { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsPublished { get; set; }
    }
}
