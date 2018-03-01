using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventorAccessPortal.Web.Models.Home
{
    public class RecentActivitiesModel
    {
        public class Item
        {
            public string Date;
            public string Activity;
            public string Details;
            public string Notes;
            public string FileNum;
            public string FileName;
            public string Status;
            public string SerialNum;
            public string LawFirm;
        }
        public class Form
        {
            public List<Item> RecentActivities = new List<Item>();
        }
    }
}