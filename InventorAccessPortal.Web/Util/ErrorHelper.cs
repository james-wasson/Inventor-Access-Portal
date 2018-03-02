using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;
using InventorAccessPortal.Web.Models;

namespace InventorAccessPortal.Web.Util
{
    public static class ErrorHelper
    {
        public static string GetErrorDescription(Enum error)
        {
            FieldInfo fi = error.GetType().GetField(error.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return "An unknown error occured.";
        }

        public static List<string> GetAllErrors(List<Enum> errors)
        {
            var rv = new List<string>();
            foreach (var err in errors)
            {
                rv.Add(GetErrorDescription(err));
            }
            return rv.Distinct().ToList();
        }

        public static bool ModelHasValidErrors(ErrorModel Model)
        {
            if (Model == null || Model.Errors == null || Model.Errors.Count <= 0)
            {
                return false;
            } 

            return true;
        }
    }
}