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
    
    public partial class Web_Action_datum
    {
        public string Investigator_Name { get; set; }
        public int Id { get; set; }
        public string Guid { get; set; }
        public string Json_Data { get; set; }
        public System.DateTime Created { get; set; }
        public Nullable<System.DateTime> Expires { get; set; }
        public ActionNumberEnum Action_Number { get; set; }
    
        public virtual Web_Action_Type Web_Action_Types { get; set; }
        public virtual Web_Login_datum Web_Login_Data { get; set; }
    }
}
