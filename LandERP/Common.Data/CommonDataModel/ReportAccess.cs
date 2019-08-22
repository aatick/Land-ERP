using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data.CommonDataModel
{
    [Table("ReportAccess")]
    public class ReportAccess
    {
        public int Id { get; set; }
        public int ReportId { get; set; }
        public int UserId { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int EntryUserId { get; set; }
        public int? UpdateUserId { get; set; }
        public bool IsActive { get; set; }
    }
}
