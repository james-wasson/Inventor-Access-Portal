using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventorAccessPortal.Web.Mailer
{
    public class EmailBaseController: Controller
    {
        private const string ViewStructure = "~/Mailer/Templates/{0}/{1}.cshtml";
        public string ErrorView = "~/Mailer/Templates/Shared/Error.cshtml";
        public string EmailView = "~/Mailer/Templates/{0}/Email.cshtml";
        public string ReceivedView = "~/Mailer/Templates/{0}/Received.cshtml";
        public string SentView = "~/Mailer/Templates/{0}/Sent.cshtml";
        private string ControllerName;

        public EmailBaseController()
        {
            ControllerName = this.GetType().Name;
            ControllerName = ControllerName.Replace("Controller", "");
            EmailView = String.Format(EmailView, ControllerName);
            ReceivedView = String.Format(ReceivedView, ControllerName);
            SentView = String.Format(SentView, ControllerName);
        }

        public string GetNewViewPath(string viewName)
        {
            return String.Format(ViewStructure, ControllerName, viewName);
        }
    }
}