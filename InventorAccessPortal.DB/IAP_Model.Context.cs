﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InventorAccessPortal.DB
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class IAP_Entities : DbContext
    {
        public IAP_Entities()
            : base("name=IAP_Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<All_Investigator> All_Investigators { get; set; }
        public virtual DbSet<Code> Codes { get; set; }
        public virtual DbSet<College> Colleges { get; set; }
        public virtual DbSet<Combo_Family> Combo_Families { get; set; }
        public virtual DbSet<Combo_Family_Listing> Combo_Family_Listings { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Ending_Fiscal_Year> Ending_Fiscal_Years { get; set; }
        public virtual DbSet<Family> Families { get; set; }
        public virtual DbSet<Family_Listing> Family_Listings { get; set; }
        public virtual DbSet<File_Number> File_Numbers { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<Investigator> Investigators { get; set; }
        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<Project_Number> Project_Numbers { get; set; }
        public virtual DbSet<Records_Status> Records_Status { get; set; }
        public virtual DbSet<Reminder> Reminders { get; set; }
        public virtual DbSet<Starting_Fiscal_Year> Starting_Fiscal_Years { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<Web_Login_datum> Web_Login_Data { get; set; }
        public virtual DbSet<Web_Action_datum> Web_Action_Data { get; set; }
        public virtual DbSet<Web_Action_Type> Web_Action_Types { get; set; }
    }
}
