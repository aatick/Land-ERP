namespace Common.Data.CommonDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Organization")]
    public partial class Organization
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public string OrganizationName { get; set; }
        public string OrganizationShortName { get; set; }
        public string OrganizationAddress { get; set; }
        public string OrganizationEmail { get; set; }
        public string OrganizationEmailPassword { get; set; }
        public string OrganizationPhone { get; set; }
        public string OrganizationFax { get; set; }
        public string OrganizationLogo { get; set; }
        public DateTime EntryDate { get; set; }
        public int EntryUserId { get; set; }
        public bool IsActive { get; set; }

    }
}
