using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InventorAccessPortal.DB.Objects;

namespace InventorAccessPortal.Web.Models
{
    public class Test
    {
       // private Connection mydb = new Connection(System.Configuration.ConfigurationManager.ConnectionStrings["Access.DB.1"].ToString(), "Access.DB.1", "Microsoft.ACE.OLEDB.12.0");
        public Connection Mydb { get; set; }
    }
}