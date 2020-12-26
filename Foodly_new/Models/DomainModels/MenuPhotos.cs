using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Foodly_new.Models.DomainModels
{
    public class MenuPhotos
    {
        [Required]
        [Key]
        public string MenuID { get; set; }
        [Required]
        public string Photo { get; set; }
    }
}
