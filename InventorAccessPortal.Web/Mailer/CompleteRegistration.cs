using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InventorAccessPortal.Web.Models.Emails;
using InventorAccessPortal.DB;
using InventorAccessPortal.DB.DataAccess;

namespace InventorAccessPortal.Web.Mailer
{
    public class CompleteRegistration: Controller 
    {
        public static bool Send(CompleteRegistrationModel model)
        {
            using (var e = new EntityContext())
            {
                ActionData.SetNewAction(Web_Action_Type)
            }
            return true;
        }
        // standardized what happens on confirm action
        public ActionResult Confirm(Guid actionGuid)
        {
            return View("~/Views/Account/EmailConfirmed.cshtml");
        }
    }
}