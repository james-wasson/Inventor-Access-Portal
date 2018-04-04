using System;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using InventorAccessPortal.Web.Models;
using InventorAccessPortal.Web.Models.Account;
using InventorAccessPortal.Web.Util;
using InventorAccessPortal.DB.Auth;
using System.Web.Security;
using InventorAccessPortal.DB;
using InventorAccessPortal.Web.Enums;
using InventorAccessPortal.DB.Objects;
using InventorAccessPortal.DB.DataAccess;

namespace InventorAccessPortal.Web.Controllers
{
    /// <summary>
    /// This Controller allows anonmous requests.
    /// Don't put anything non-public here
    /// </summary>
    [AllowAnonymous]
    public class AccountController : Controller
    {

        public ActionResult Login()
        {
            return View(new LoginModel());
        }

        [ValidateAntiForgeryToken, HttpPost]
        public ActionResult Login(LoginModel model)
        {
            // clears the errors from the model
            model.ClearToaster();
            // checks if the user passed in their login data
            if (!String.IsNullOrEmpty(model.UsernameOrEmail) && !String.IsNullOrEmpty(model.Password))
            {
                using (var e = new EntityContext()) // db context
                {
                    //check username and password from database
                    CachedUser cachedUser = null;
                    var isEmail = CredentialsHelper.IsEmailValid(model.UsernameOrEmail);
                    if (isEmail) // is an email
                        cachedUser = Authorize.CredentialsByEmail(model.UsernameOrEmail, model.Password, e);
                    else // is username
                        cachedUser = Authorize.CredentialsByUsername(model.UsernameOrEmail, model.Password, e);
                    
                    if (cachedUser != null)
                    {
                        //if username and password is correct, create session and return Success
                        SessionHelper.SetSessionUser(cachedUser);
                        FormsAuthentication.SetAuthCookie(cachedUser.Username, true);
                        
                        // goes to home screen or previous screen
                        FormsAuthentication.RedirectFromLoginPage(cachedUser.Username, true);
                    }
                    // check if we can give any more detail to errors
                    var errors = Authorize.GetAuthorizeErrors();
                    if (!errors.Any()) // if no errors, throw unknown error
                    {
                        model.AddError(LoginErrors.UnknownError);
                    }
                    // if the user does not have the right username and password, don't give any more info
                    else if (errors.Contains(Authorize.AuthorizeErrorsEnum.PasswordNotVerified) ||
                       errors.Contains(Authorize.AuthorizeErrorsEnum.NoLoginData))
                    {
                        model.AddError(LoginErrors.InvalidUsernameOrPassword);
                    }
                    else // checks to see if we can find another issue
                    {
                        if (errors.Contains(Authorize.AuthorizeErrorsEnum.EmailNotConfirmed))
                            model.AddError(LoginErrors.EmailNotConfirmed);
                        if (errors.Contains(Authorize.AuthorizeErrorsEnum.LoginSuspended))
                            model.AddError(LoginErrors.Suspended);
                    }
                }
            }
            else
            {
                // throws a EmptyUsernameOrPassword error
                model.AddError(GlobalErrors.EmptyFields);
            }
            // if we got here there was an error
            return View(model);
        }

        public ActionResult LogOff()
        {
            AccountHelper.Logout(HttpContext);
            return View();
        }

        public ActionResult Register()
        {
            return View(new RegisterModel());
        }


        [ValidateAntiForgeryToken, HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            // clears the errors from the model
            model.ClearToaster();
            // check for simple warnings
            var isValid = true;
            // makes sure we don't have any empty fields
            if (String.IsNullOrEmpty(model.Username) || String.IsNullOrEmpty(model.Password) || String.IsNullOrEmpty(model.Email))
            {
                model.AddError(GlobalErrors.EmptyFields);
                isValid = false;
            }
            if (!CredentialsHelper.IsEmailValid(model.Email)) // check email is valid
            {
                model.AddError(RegistrationErrors.InvalidEmail);
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

            if (!CredentialsHelper.IsUsernameValid(model.Username)) // check if username is valid
            {
                model.AddError(RegistrationErrors.InvalidUsername);
                isValid = false;
            }

            if (isValid) // check for more serious warnings
            {
                using (var e = new EntityContext()) // db context
                {
                    // check if email exists in the database, we need the email to register
                    if (!Authorize.EmailExists(model.Email, e)) 
                    {
                        model.AddError(RegistrationErrors.EmailNotAssociatedWithUser);
                        isValid = false;
                    }
                    else if (Authorize.EmailIsRegistered(model.Email, e)) // if it does check if it is already registered
                    {
                        model.AddError(RegistrationErrors.EmailAlreadyExists);
                        isValid = false;
                    }
                    else if (Authorize.UsernameIsRegistered(model.Username, e)) // check if the username is already registered
                    {
                        model.AddError(RegistrationErrors.UsernameAlreadyExists);
                        isValid = false;
                    }

                    if (isValid && !model.HasWarnings()) // we have checked everything we need to check
                    {
                        CachedUser cachedUser = Account.MakeNewUserLogin(model.Username, model.Email, model.Password, e);
                        if (cachedUser == null)
                        {
                            model.AddError(RegistrationErrors.UnknowError);
                        }
                        else
                        {
                            return RedirectToAction("Send", "CompleteRegistration", new {
                                email = cachedUser.Email,
                                username = cachedUser.Username,
                                investigatorName = cachedUser.InvestigatorName
                            });
                        }
                    }
                }
            }
            // if we got here there was an error
            return View(model);
        }

        public ActionResult ResetPassword()
        {
            return View(new ResetPasswordModel());
        }

        [ValidateAntiForgeryToken, HttpPost]
        public ActionResult ResetPassword(ResetPasswordModel model)
        {
            // clears the errors from the model
            model.ClearToaster();
            // check for simple warnings
            var isValid = true;
            // makes sure we don't have any empty fields
            if (String.IsNullOrEmpty(model.Email))
            {
                model.AddError(GlobalErrors.EmptyFields);
                isValid = false;
            }
            if (!CredentialsHelper.IsEmailValid(model.Email)) // check email is valid
            {
                model.AddError(ResetPasswordErrors.InvalidEmail);
                isValid = false;
            }

            if (isValid) // check for more serious warnings
            {
                using (var e = new EntityContext()) // db context
                {
                    // check if email exists in the database, we need the email to register
                    if (!Authorize.EmailExists(model.Email, e))
                    {
                        model.AddError(ResetPasswordErrors.EmailNotAssociatedWithUser);
                        isValid = false;
                    }
                    else if (!Authorize.EmailIsRegistered(model.Email, e)) // if it does check if it is already registered
                    {
                        model.AddError(ResetPasswordErrors.EmailNotRegistered);
                        isValid = false;
                    }

                    if (isValid && !model.HasWarnings()) // we have checked everything we need to check
                    {
                        CachedUser cachedUser = GetCachedUser.UserByEmail(model.Email, e);
                        if (cachedUser == null)
                        {
                            model.AddError(RegistrationErrors.UnknowError);
                        }
                        else
                        {
                            return RedirectToAction("Send", "ResetPassword", new
                            {
                                email = cachedUser.Email,
                                username = cachedUser.Username,
                                investigatorName = cachedUser.InvestigatorName
                            });
                        }
                    }
                }
            }
            // if we got here there was an error
            return View(model);
        }
        public ActionResult LoginResetPassword()
        {
            return View(new LoginResetPasswordModel());
        }

        [ValidateAntiForgeryToken, HttpPost]
        public ActionResult LoginResetPassword(LoginResetPasswordModel model)
        {
            // clears the errors from the model
            model.ClearToaster();
            // check for simple warnings
            var isValid = true;
            // makes sure we don't have any empty fields
            if (String.IsNullOrEmpty(model.Password))
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


            if (isValid && !model.HasWarnings())
            {
                using (var e2 = new EntityContext()) // db context
                {
                    var currentUser = SessionHelper.GetSessionUser();
                    if (currentUser == null)
                    {
                        model.AddError(GlobalErrors.ServerError);
                        return View(model);
                    }
                    var success = Authorize.ResetPassword(currentUser.Email, model.Password, e2);
                    var newUser = Authorize.CredentialsByEmail(currentUser.Email, model.Password, e2);
                    if (!success || newUser == null)
                    {
                        model.AddError(GlobalErrors.ServerError);
                        return View(model);
                    }
                    else
                    {
                        //if username and password is correct, create session and return Success
                        SessionHelper.SetSessionUser(newUser);
                        FormsAuthentication.SetAuthCookie(newUser.Username, true);
                        model = new LoginResetPasswordModel();
                        model.AddSuccess(ResetPasswordSuccessEnum.PasswordReset);
                        return View(model);
                    }
                }
            }
            // if we got here there was an error
            return View(model);
        }
    }
}