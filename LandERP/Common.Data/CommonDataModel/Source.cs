using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data.CommonDataModel
{
    [Table("Source")]
    public partial class Source
    {
        public int Id { get; set; }
        public string SourceName { get; set; }
        public bool IsActive { get; set; }
    }
}
