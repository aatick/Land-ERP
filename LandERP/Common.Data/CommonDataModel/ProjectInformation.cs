using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Data.CommonDataModel
{
    [Table("ProjectInformation")]
    public partial class ProjectInformation
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectShortName { get; set; }
        public string ProjectAddress { get; set; }
        public string Facility { get; set; }
        public string Fittings { get; set; }
        public int? CategoryId { get; set; }
        public int? TotalStoried { get; set; }
        public int? TotalUnit { get; set; }
        public decimal PricePerSft { get; set; }
        public decimal? CarParkingPrice { get; set; }
        public decimal? Utility { get; set; }
        public int? PriceSetup { get; set; }
        public decimal? BookingMoney { get; set; }
        public decimal? DownPayment { get; set; }
        public int? TotalCarParking { get; set; }
        public int? ProjectStatusId { get; set; }
        public DateTime DelayChargeStartDate { get; set; }
        public decimal DelayChargeRate { get; set; }
        public int FileNoSetup { get; set; }
        public int UnitId { get; set; }
        public int MoneyReceiptSetup { get; set; }
        public int? ProjectParentId { get; set; }
        public DateTime EntryDate { get; set; }
        public int EntryUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? UpdateUserId { get; set; }
        public bool IsActive { get; set; }
    }
}
