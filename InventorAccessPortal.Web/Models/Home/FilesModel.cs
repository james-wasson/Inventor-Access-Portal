using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventorAccessPortal.Web.Models.Home
{
    public class FilesModel
    {
        public class Item
        {
            // Transactions table
            public string Status;
            public string FileNum;
            public string FileName;
            // FileNumbers Table
            public string SerialNum;
            public string Continuity;
            public string LawFirm;
            public string ProjectNum;
        }
        public class Form : SharedModel
        {
            public List<Item> Files = new List<Item>();
        }
    }
}