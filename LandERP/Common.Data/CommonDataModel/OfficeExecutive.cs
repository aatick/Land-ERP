using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data.CommonDataModel
{
    [Table("OfficeExecutive")]
    public partial class OfficeExecutive
    {
        public int Id { get; set; }
        public string ExecutiveName { get; set; }
        public string ExecutiveCode { get; set; }
        public int OrganizationId { get; set; }
        public int DesignationId { get; set; }
        public int DepartmentId { get; set; }
        public DateTime JoiningDate { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public int GenderId { get; set; }
        public string PresentAddress { get; set; }
        public int? PresentThanaId { get; set; }
        public string PermanentAddress { get; set; }
        public int? PermanentThanaId { get; set; }
        public int CountryId { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }

        [Column(TypeName = "image")]
        public byte[] Photograph { get; set; }

        [Column(TypeName = "image")]
        public byte[] Signature { get; set; }

        public int? TeamId { get; set; }
        public DateTime EntryDate { get; set; }
        public int EntryUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? UpdateUserId { get; set; }
        public bool IsActive { get; set; }
    }
}
