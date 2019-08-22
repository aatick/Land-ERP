using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data.CommonDataModel
{
    [Table("Country")]
    public partial class Country
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string CountryName { get; set; }
        public bool IsActive { get; set; }
    }
}
