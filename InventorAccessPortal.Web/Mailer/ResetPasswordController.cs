using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InventorAccessPortal.DB;
using InventorAccessPortal.DB.DataAccess;
using InventorAccessPortal.DB.Objects;
using InventorAccessPortal.DB.Auth;
using System.Web.Razor;
using InventorAccessPortal.Web.Models.Account;
using InventorAccessPortal.Web.Util;
using System.Web.Security;
using InventorAccessPortal.Web.Enums;
using InventorAccessPortal.Web.Mailer;
using InventorAccessPortal.Web.Mailer.Models.ResetPassword;
namespace InventorAccessPortal.Web.Mailer
{
    [AllowAnonymous]
    public class ResetPasswordController : EmailBaseController
    {
        String ResetPasswordSuccessView;
        String ResetPasswordErrorView;
        public ResetPasswordController()
        {
            ResetPasswordErrorView = GetNewViewPath("ResetPasswordError");
            ResetPasswordSuccessView = GetNewViewPath("ResetPasswordSuccess");
        }
        [ValidateAntiForgeryToken, HttpPost]
        public ActionResult ActualResetPassword(DataModel model)
        {
                // clears the errors from the model
                model.ClearErrorAndWarning();
                // check for simple warnings
                var isValid = true;
                // makes sure we don't have any empty fields
                if (String.IsNullOrEmpty(model.Password) || String.IsNullOrEmpty(model.Email))
                {
                    model.AddError(GlobalErrors.EmptyFields);
                    isValid = false;
                }
                if (!CredentialsHelper.IsPasswordValid(model.Password)) // check password is valid
                {
                    model.AddError(RegistrationErrors.InvalidPassword);
                    isValid = false;
                }
                else // if password is valid get warnings
                {
                    model.AddWarnings(CredentialsHelper.GetPasswordWarnings(model.Password));
                }


                if (isValid) // check for more serious warnings
                {
                    using (var e = new EntityContext()) // db context
                    {
                        if (isValid && !model.HasWarnings()) // we have checked everything we need to check
                        {
                            var success = Authorize.ResetPassword(model.Email, model.Password, e);
                            if (!success)
                            {
                                return View(ResetPasswordErrorView);
                            }
                            else
                            {
                                return View(ResetPasswordSuccessView);
                            }
                        }
                    }
                }
            // if we got here there was an error
            return View(ReceivedView, model);
        }


        public ActionResult Send(String email, String username, String investigatorName)
        {
            if (email == null || username == null || investigatorName == null)
                return View(ErrorView);
            var dataModel = new DataModel()
            {
                Email = email
            };
            // set Action in database
            Guid? guid = null;
            using (var e = new EntityContext())
            {
                guid = ActionData.SetNewAction(new NewActionData()
                {
                    ActionNumber = ActionNumberEnum.ResetPassword,
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
            var subject = "Password Reset, Inventor Access Portal";
            var mailSent = SendEmail.Send(subject, bodyHtml, email, true);
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
                    return View(ResetPasswordErrorView);
                }

                e.Web_Action_Data.Remove(actionRow);
                e.SaveChanges();

                return View(ReceivedView, model);
            }
        }
    }
}