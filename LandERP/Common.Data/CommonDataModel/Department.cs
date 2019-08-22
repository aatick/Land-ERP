using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data.CommonDataModel
{
    [Table("Department")]
    public partial class Department
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public bool IsActive { get; set; }
    }
}
