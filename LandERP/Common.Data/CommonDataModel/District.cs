using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data.CommonDataModel
{
    [Table("District")]
    public partial class District
    {
        public int Id { get; set; }

        public int DivisionId { get; set; }

        [Required]
        [StringLength(50)]
        public string DistrictName { get; set; }

        public bool IsActive { get; set; }
    }
}
