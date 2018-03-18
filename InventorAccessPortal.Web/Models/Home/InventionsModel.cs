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
            public int ProjectNumber;
            public string ProjectTitle;
        }
        public class Form : _TitleModel
        {
            public List<Item> Inventions = new List<Item>();
        }
    }
}