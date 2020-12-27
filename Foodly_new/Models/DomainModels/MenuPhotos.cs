using System.ComponentModel.DataAnnotations;

namespace Foodly_new.Models.DomainModels
{
    public class MenuPhotos
    {
        [Required]
        [Key]
        public string PhotoID { get; set; }
        [Required]
        public string MenuID { get; set; }
        [Required]
        public string Photo { get; set; }
    }
}
