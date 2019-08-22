using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data.CommonDataModel
{
    [Table("EmailSMSType")]
    public class EmailSMSType
    {
        public int Id { get; set; }
        public string EmailSMSTypeName { get; set; }
        public string EmailSMSTypeShortName { get; set; }
        public bool IsEmail { get; set; }
        public DateTime EntryDate { get; set; }
        public int EntryUserId { get; set; }
        public bool IsActive { get; set; }
    }
}
