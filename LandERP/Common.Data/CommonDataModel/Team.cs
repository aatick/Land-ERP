using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data.CommonDataModel
{
    [Table("Team")]
    public class Team
    {
        public int Id { get; set; }
        public string TeamName { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int EntryUserId { get; set; }
        public int? UpdateUserId { get; set; }
        public bool IsActive { get; set; }

    }
}
