namespace Common.Data.CommonDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Profession")]
    public partial class Profession
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string ProfessionName { get; set; }

        public bool IsActive { get; set; }
    }
}
