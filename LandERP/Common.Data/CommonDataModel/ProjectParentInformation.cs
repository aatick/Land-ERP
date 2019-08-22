using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data.CommonDataModel
{
    [Table("ProjectParentInformation")]
    public class ProjectParentInformation
    {
        public int Id { get; set; }
        public string ProjectParentName { get; set; }
        public string ProjectParentShortName { get; set; }
        public string Address { get; set; }
        public int OrganizationId { get; set; }
        public DateTime EntryDate { get; set; }
        public int EntryUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? UpdateUserId { get; set; }
        public bool IsActive { get; set; }
    }
}
