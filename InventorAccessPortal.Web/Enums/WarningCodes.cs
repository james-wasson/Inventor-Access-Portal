using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace InventorAccessPortal.Web.Enums
{
    public enum PasswordWarnings
    {
        [Description ("Password is suggested to have at least one upper and lower case.")]
        UpperAndLower = 0,
        [Description("Password is suggested to have at least one number.")]
        Number = 1,
        [Description("Password is suggested to have at least one special character [~, !, +, -, {, }, [, ], ., ?, *, _]")]
        SpecialCharacter = 2
    }
}