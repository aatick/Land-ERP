namespace Common.Data.CommonDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Relation")]
    public partial class Relation
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string RelationName { get; set; }
        public bool HasClientForm { get; set; }
        public bool IsActive { get; set; }
    }
}
