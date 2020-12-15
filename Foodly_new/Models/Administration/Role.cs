using System.ComponentModel.DataAnnotations;

namespace Foodly_new.Models.Administration
{
    public class Role
    {
        [Required]
        public string RoleName { get; set; }
    }
}
