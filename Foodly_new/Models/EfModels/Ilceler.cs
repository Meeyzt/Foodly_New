using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Foodly_new.Models.EfModels
{
    public class Ilceler
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IlceID { get; set; }
        public string Ilce { get; set; }
        public int IlID { get; set; }        
    }
}
