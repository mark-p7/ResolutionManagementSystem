using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResolutionManagement.Models
{
    public class FeedbackRequest
    {
        [Required(ErrorMessage = "Feedback Request ID is required")]
        [Display(Name = "Feedback Request ID")]
        public int? FeedbackRequestId { get; set; }
        [Display(Name = "Creation Date")]
        public DateTime? CreationDate { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [StringLength(1000, ErrorMessage = "Description cannot be longer than 1000 characters.")]
        [Display(Name = "Description")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "ESignature is required")]
        public string? ESignature { get; set; }
        [Display(Name = "Owner's ID")]
        public string? OwnerUserID { get; set; }
        public bool? Resolved { get; set; }
        public bool? Accepted { get; set; }
        [Display(Name = "Resolution ID")]
        public int? ResolutionId { get; set; }
        [ForeignKey("ResolutionId")]
        public Resolution? Resolution { get; set; }
    }
}