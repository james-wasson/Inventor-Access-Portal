using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventorAccessPortal.Web.Models.Home
{
    public class InventionsModel
    {
        public class Item
        {
            public string Status;
            public string ProjectNumber;
            public string ProjectTitle;
        }
        public class Form
        {
            public List<Item> Inventions = new List<Item>();
        }
    }
}