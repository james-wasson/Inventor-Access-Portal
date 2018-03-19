using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InventorAccessPortal.Web.Mailer.Models.CompleteRegistration;
using InventorAccessPortal.DB;
using InventorAccessPortal.DB.DataAccess;
using InventorAccessPortal.DB.Objects;
using InventorAccessPortal.DB.Auth;
using System.Web.Razor;
using System.Net.Mail;
using System.IO;
using CssInliner;

namespace InventorAccessPortal.Web.Mailer
{
    [AllowAnonymous]
    public class CompleteRegistrationController: EmailBaseController
    {
        String ConfirmationErrorView;
        public CompleteRegistrationController()
        {
            ConfirmationErrorView = GetNewViewPath("ConfirmationError");
        }

        public ActionResult Send(String email, String username, String investigatorName)
        {
            if (email == null || username == null || investigatorName == null)
                return View(ErrorView);
            var dataModel = new DataModel()
            {
                Email = email,
                Username = username,
                InvestigatorName = investigatorName
            };
            // set Action in database
            Guid? guid = null;
            using (var e = new EntityContext())
            {
                guid = ActionData.SetNewAction(new NewActionData()
                {
                    ActionNumber = ActionNumberEnum.Register,
                    InvestigatorName = investigatorName,
                    Model = dataModel
                }, e);
                if (guid == null)
                {
                    return View(ErrorView);
                }
            }
            var emailModel = new EmailModel()
            {
                ActionGuid = guid ?? default(Guid)
            };
            // send mail
            var bodyHtml = RazorExtensions.RazorToHtml(EmailView, emailModel, this.ControllerContext);
            var subject = "Inventor Access Portal, Technology of Techtransfer Account Registration";
            var mailSent = SendEmail.Send(subject, bodyHtml, "jameswasson1@gmail.com", true);
            if (!mailSent)
            {
                return View(ErrorView);
            }
            return View(SentView);
        }
        // standardized what happens on confirm action
        public ActionResult Received(String actionGuid)
        {
            using (var e = new EntityContext())
            {
                var data = ActionData.GetAction<DataModel>(Guid.Parse(actionGuid), e);
                var model = data.Item1;
                var actionRow = data.Item2;
                if (model == null || actionRow == null || actionRow.Investigator_Name == null)
                {
                    return View(ConfirmationErrorView);
                }
                var success = Authorize.ConfirmEmail(model.Email, model.Username, actionRow.Investigator_Name, e);
                if (!success)
                {
                    return View(ConfirmationErrorView);
                }
                e.Web_Action_Data.Remove(actionRow);
                e.SaveChanges();
            }

            return View(ReceivedView);
        }
    }
}