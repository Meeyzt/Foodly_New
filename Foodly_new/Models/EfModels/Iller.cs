using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Foodly_new.Models.EfModels
{
    public class Iller
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IlID { get; set; }
        public string Il { get; set; }
    }
}
