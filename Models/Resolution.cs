using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace ResolutionManagement.Models
{
    public class Resolution
    {
        [Key]
        [Display(Name = "Resolution ID")]
        public int? ResolutionId { get; set; }
        [Display(Name = "Creation Date")]
        public DateTime? CreationDate { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [StringLength(1000, ErrorMessage = "Description cannot be longer than 1000 characters.")]
        [Display(Name = "Description")]
        public string? Abstract { get; set; }
        [Display(Name = "Status")]
        public string? Status { get; set; }
        [Display(Name = "Owner's ID")]
        public string? OwnerUserID { get; set; }
    }
}