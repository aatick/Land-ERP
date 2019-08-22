using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data.CommonDataModel
{
    [Table("GroupSetup")]
    public partial class GroupSetup
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public string GroupAddress { get; set; }
        public string GroupPhone { get; set; }
        public string GroupEmail { get; set; }
        public string GroupFax { get; set; }
        public string GroupLogo { get; set; }
        public string SMSMobileNo { get; set; }
        public string SMSUserName { get; set; }
        public string SMSPassword { get; set; }
        public DateTime EntryDate { get; set; }
        public int EntryUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? UpdateUserId { get; set; }
        public bool IsActive { get; set; }
    }
}
