using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventorAccessPortal.Web.Models
{
    /// <summary>
    /// Sets the basic format for returning errors to the page
    /// </summary>
    public class ErrorModel
    {
        public List<Enum> Errors = new List<Enum>();
    }
}