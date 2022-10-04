using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ResolutionManagement.Models
{
    public class FeedbackRequest
    {
        public int? FeedbackRequestId { get; set; }
        public DateTime? CreationDate { get; set; }
        public string? OwnerUserID { get; set; }
        public int? ResolutionId { get; set; }
        [ForeignKey("ResolutionId")]
        public Resolution? Resolution { get; set; }
    }
}