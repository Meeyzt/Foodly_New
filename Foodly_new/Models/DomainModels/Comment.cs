using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Foodly_new.Models.DomainModels
{
    public class Comment
    {
        [Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string CommentID { get; set; }
        [Required]
        public DateTime PublishDate { get; set; }
        [Required]
        public string Entry { get; set; }
        [Required]
        public string ReviewID { get; set; }
        [Required]
        public string UserID { get; set; }
    }
}
