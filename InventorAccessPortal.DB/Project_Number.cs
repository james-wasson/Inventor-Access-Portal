//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Project_Number
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Project_Number()
        {
            this.All_Investigators = new HashSet<All_Investigator>();
            this.File_Numbers = new HashSet<File_Number>();
        }
    
        public int Project_Number1 { get; set; }
        public string Project_Name { get; set; }
        public string Project_Title { get; set; }
        public string Lead_Investigator { get; set; }
        public string Status { get; set; }
        public string Records_Status { get; set; }
        public Nullable<System.DateTime> Destruction_Date { get; set; }
        public Nullable<int> Starting_Fiscal_Year { get; set; }
        public Nullable<int> Ending_Fiscal_Year { get; set; }
        public string DataSource { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<All_Investigator> All_Investigators { get; set; }
        public virtual Ending_Fiscal_Year Ending_Fiscal_Year1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<File_Number> File_Numbers { get; set; }
        public virtual Investigator Investigator { get; set; }
        public virtual Records_Status Records_Status1 { get; set; }
        public virtual Starting_Fiscal_Year Starting_Fiscal_Year1 { get; set; }
        public virtual Status Status1 { get; set; }
    }
}
