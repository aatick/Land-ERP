using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data.CommonDataModel
{
    [Table("BankBranch")]
    public partial class BankBranch
    {
        public int Id { get; set; }
        public int? ThanaId { get; set; }
        public int BankId { get; set; }
        public string BranchName { get; set; }
        public string Address { get; set; }
        public string RoutingNo { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int EntryUserId { get; set; }
        public int? UpdateUserId { get; set; }
        public bool IsActive { get; set; }
    }
}
