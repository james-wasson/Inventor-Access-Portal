using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor;
using System.IO;
using System.Net.Mail;
using System.Configuration;
using System.Net.Configuration;

namespace InventorAccessPortal.Web.Mailer
{

    public static class SendEmail
    {
        public static int NumberOfSentEmails = 1;
        public static SmtpClient Client = new SmtpClient();
        public static SmtpSection SmtpData = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
        public static MailAddress FromAddress = SmtpData.From != null ? new MailAddress(SmtpData.From) : null;

        public static bool Send(String subject, String body, MailAddress toAddres, bool isHtml = true)
        {
            try // returns true if mail was sent
            {
                // if it is html, move styles to inline styles
                if (isHtml) try { body = CssInliner.Inliner.InlineCssIntoHtml(body); } catch (Exception ex) { }

                MailMessage mail = new MailMessage(FromAddress, toAddres);
                mail.Subject = subject + " [" + NumberOfSentEmails.ToString() + "]";
                mail.Body = body;
                mail.IsBodyHtml = isHtml;
                Client.Send(mail);
                NumberOfSentEmails += 1;
                return true;
            }
            catch (Exception ex) // false otherwise
            {
                return false;
            }

        }
        public static bool Send(String subject, String body, String toAddres, bool isHtml = true)
        {
            return Send(subject, body, new MailAddress(toAddres), isHtml);
        }
    }
}

