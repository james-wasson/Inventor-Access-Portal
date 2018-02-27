using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventorAccessPortal.Web.Models.Home
{
    public class Files
    {
        public class Item
        {
            public string Status;
            public string FileNum;
            public string FileName;
            public string SerialNum;
            public string Continuity;
            public string LawFirm;
            public string ProjectNum;
        }
        public class Form
        {
            public List<Item> Files;
        }
    }
}