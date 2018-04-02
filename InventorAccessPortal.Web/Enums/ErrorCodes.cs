using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace InventorAccessPortal.Web.Enums
{
    public enum RegistrationErrors {
        [Description("Invalid Password, Passwords length must be between 16 and 255 characters.")]
        InvalidPassword = 0,
        [Description("Invalid Username, Username must be between 4 and 255 characters and not contain charcters: @ , or space")]
        InvalidUsername = 1,
        [Description("Invalid Email, Email must be in format: user@domain.(com, edu, net, etc.) and less than 255 characters")]
        InvalidEmail = 2,
        [Description("This Username has already been registered.")]
        UsernameAlreadyExists = 3,
        [Description("This Email has already been registered.")]
        EmailAlreadyExists = 4,
        [Description("This Email is not assigned a user and cannot be Registered. If you need to setup an account please contact: support@domain.com")]
        EmailNotAssociatedWithUser = 5,
        [Description("Unknow Registration Error.")]
        UnknowError = 6
    }

    public enum LoginErrors {
        [Description("Login Suspended.")]
        Suspended = 0,
        [Description("Email Not Confirmed.")]
        EmailNotConfirmed = 1,
        [Description("Invalid Username or Password.")]
        InvalidUsernameOrPassword = 2,
        [Description("An Unknown Login Error Occured.")]
        UnknownError = 3,
    }

    public enum ResetPasswordErrors
    {
        [Description("This Email is not assigned a user and is not Registered. If you need to setup an account please contact: support@domain.com")]
        EmailNotAssociatedWithUser = 0,
        [Description("Invalid Email, Email must be in format: user@domain.(com, edu, net, etc.) and less than 255 characters")]
        InvalidEmail = 1,
        [Description("This Email has not been registered yet")]
        EmailNotRegistered = 2
    }

    public enum GlobalErrors {

        [Description("Required fields cannot be empty.")]
        EmptyFields = 0
    }
}