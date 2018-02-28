using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventorAccessPortal.Web.Models.Home
{
    public class Families
    {
        public class Item
        {
            public string Status;
            public string FamilyNum;
            public string FamilyName;
        }
        public class Form
        {
            public List<Item> Families;
        }
    }
}