using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventorAccessPortal.Web.Models
{
    /// <summary>
    /// Sets the basic format for returning errors to the page
    /// Extend this class and populate the Errors list to get errors to show up
    /// </summary>
    public class ErrorModel
    {
        public List<Enum> _Errors = new List<Enum>();
    }
}