using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventorAccessPortal.Web.Models
{
    /// <summary>
    /// Sets the basic format for extending a model
    /// </summary>
    public class _TitleModel : _ToasterModel
    {
        // extended title
        public String ExtendedTitle { get; private set; } = "";
        public void SetExtendedTitle(String t)
        {
            if (String.IsNullOrEmpty(t))
            {
                ExtendedTitle = "";
            }
            else
            {
                ExtendedTitle = "For " + t;
            }
        }
        public bool HasExtendedTitle()
        {
            return !String.IsNullOrEmpty(ExtendedTitle);
        }
    }
}