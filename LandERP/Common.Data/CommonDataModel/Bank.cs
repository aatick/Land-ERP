using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data.CommonDataModel
{
    [Table("Bank")]
    public partial class Bank
    {
        public int Id { get; set; }
        public string BankShortName { get; set; }
        public string BankName { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int EntryUserId { get; set; }
        public int? UpdateUserId { get; set; }
        public bool IsActive { get; set; }
    }
}
