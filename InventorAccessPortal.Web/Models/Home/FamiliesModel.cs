﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventorAccessPortal.Web.Models.Home
{
    public class FamiliesModel
    {
        public class Item
        {
            public string Status;
            public string FileName;
            public string FamilyNum;
            public string FamilyName;
        }
        public class Form
        {
            public List<Item> Families = new List<Item>();
        }
    }
}