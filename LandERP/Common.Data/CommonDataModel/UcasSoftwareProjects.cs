namespace Common.Data.CommonDataModel
{
    // using UcasPortfolioCodeFirstMigration.Db;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UcasSoftwareProjects")]
    public partial class UcasSoftwareProjects
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public string ProjectShortName { get; set; }
        public string StyleCss { get; set; }
        public string ProjectHomePage { get; set; }
        public bool? IsActive { get; set; }
       
    }
}
