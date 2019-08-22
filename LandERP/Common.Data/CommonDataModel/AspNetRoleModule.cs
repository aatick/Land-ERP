namespace Common.Data.CommonDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AspNetRoleModule")]
    public partial class AspNetRoleModule
    {
        public int AspNetRoleModuleId { get; set; }

        public int RoleId { get; set; }

        public int ModuleId { get; set; }
        
        public int EntryUserId { get; set; }

        public DateTime EntryDate { get; set; }

        public bool IsActive { get; set; }
    }
}
