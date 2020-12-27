using System;
using System.ComponentModel.DataAnnotations;

namespace Foodly_new.Models.HomeModels
{
    public class Values
    {
        public string ReviewID { get; set; }
        public string BannerImage { get; set; }
        [Required]
        public string ShortCast { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public double Star { get; set; }
        [Required]
        public string Header { get; set; }
        public string classLibrary { get; set; }

    }
}
