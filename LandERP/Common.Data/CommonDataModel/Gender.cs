using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data.CommonDataModel
{
    [Table("Gender")]
    public partial class Gender
    {
        public int Id { get; set; }
        public string GenderName { get; set; }
        public bool IsActive { get; set; }
    }
}
