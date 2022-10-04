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
        public int? ResolutionId { get; set; }
        public DateTime? CreationDate { get; set; }
        public string? Abstract { get; set; }
        public string? Status { get; set; }
        public string? OwnerUserID { get; set; }
    }
}