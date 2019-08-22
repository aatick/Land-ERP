namespace Common.Data.CommonDataModel
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Common.Data.CommonDataModel;
    using BasicDataAccess;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Configuration;
    using System.Security.Cryptography;
    using System.Text;

    public partial class CommonDbContext : DbContext
    {
        public CommonDbContext()
            : base("name=ERPDbContext")
        {
        }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetRoleModule> AspNetRoleModules { get; set; }
        public virtual DbSet<AspNetSecurityModule> AspNetSecurityModules { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<ApplicationLog> ApplicationLogs { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Designation> Designations { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<Division> Divisions { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<GroupSetup> GroupSetups { get; set; }
        public virtual DbSet<OfficeExecutive> OfficeExecutives { get; set; }
        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<Profession> Professions { get; set; }
        public virtual DbSet<Relation> Relations { get; set; }

        public virtual DbSet<ReportAccess> ReportAccesses { get; set; }
        public virtual DbSet<ReportInformation> ReportInformations { get; set; }
        public virtual DbSet<Thana> Thanas { get; set; }
        public virtual DbSet<UcasSoftwareProjects> UcasSoftwareProjectses { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Bank> Banks { get; set; }
        public virtual DbSet<BankBranch> BankBranches { get; set; }
        public virtual DbSet<Source> Sources { get; set; }
        public virtual DbSet<ProjectInformation> ProjectInformations { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<EmailSMSType> EmailSmsTypes { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetSecurityModule>()
                .Property(e => e.SecurityModuleCode)
                .IsUnicode(false);

            modelBuilder.Entity<AspNetSecurityModule>()
                .Property(e => e.LinkText)
                .IsUnicode(false);

            modelBuilder.Entity<AspNetSecurityModule>()
                .Property(e => e.ControllerName)
                .IsUnicode(false);

            modelBuilder.Entity<AspNetSecurityModule>()
                .Property(e => e.ActionName)
                .IsUnicode(false);

            modelBuilder.Entity<AspNetSecurityModule>()
                .Property(e => e.AreaName)
                .IsUnicode(false);
            modelBuilder.Entity<ReportInformation>().ToTable("ReportInformation");
            modelBuilder.Entity<ReportAccess>().ToTable("ReportAccess");
            modelBuilder.Entity<AspNetSecurityModule>().ToTable("AspNetSecurityModule");
        }
    }
}

