namespace Common.Data.CommonDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Thana")]
    public partial class Thana
    {
        public int Id { get; set; }

        public int DistrictId { get; set; }

        [Required]
        [StringLength(50)]
        public string ThanaName { get; set; }
        public bool IsActive { get; set; }
    }
}
