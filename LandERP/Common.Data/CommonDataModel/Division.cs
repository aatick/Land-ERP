namespace Common.Data.CommonDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Division")]
    public partial class Division
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string DivisionName { get; set; }

        public bool IsActive { get; set; }
    }
}
