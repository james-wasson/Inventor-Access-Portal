using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace System.Web.Razor
{
    public class RazorExtensions
    {
        public static string RazorToHtml(string viewToRender, object model, ControllerContext controllerContext)
        {
            var result = ViewEngines.Engines.FindView(controllerContext, viewToRender, null);

            StringWriter output;
            using (output = new StringWriter())
            {
                var viewData = new ViewDataDictionary(model);
                if (controllerContext == null || result.View == null || viewData == null || controllerContext.Controller.TempData == null)
                {
                    if (controllerContext == null)
                        return "Error1";
                    if (result.View == null)
                        return "Error2";
                    if (viewData == null)
                        return "Error3";
                    if (controllerContext.Controller.TempData == null)
                        return "Error4";
                }
                else
                {
                    var viewContext = new ViewContext(controllerContext, result.View, viewData, controllerContext.Controller.TempData, output);
                    result.View.Render(viewContext, output);
                    result.ViewEngine.ReleaseView(controllerContext, result.View);
                }
            }

            return output.ToString();
        }
    }
}