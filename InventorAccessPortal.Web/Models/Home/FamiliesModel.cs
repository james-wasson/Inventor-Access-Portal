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
            public string FamilyNum;
            public string FamilyName;
        }
        public class Form : _TitleModel
        {
            public List<Item> Families = new List<Item>();
        }
    }
}