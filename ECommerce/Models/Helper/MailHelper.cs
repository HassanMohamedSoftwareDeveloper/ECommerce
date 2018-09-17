﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
namespace ECommerce.Models.Helper
{
    public static class MailHelper
    {
        public static async Task SendMail(string to,string Subject,string Body)
        {
            var message = new MailMessage();
            message.To.Add(new MailAddress(to));
            message.From = new MailAddress(WebConfigurationManager.AppSettings["AdminUser"]);
            message.Subject = Subject;
            message.Body = Body;
            message.IsBodyHtml = true;
            using (var smtp=new SmtpClient())
            {
                var credential = new NetworkCredential()
                {
                    UserName = WebConfigurationManager.AppSettings["AdminUser"],
                    Password= WebConfigurationManager.AppSettings["AdminPassword"]
                };
                smtp.Credentials = credential;
                smtp.Host = WebConfigurationManager.AppSettings["SMTPName"];
                smtp.Port = int.Parse(WebConfigurationManager.AppSettings["SMTPPort"]);
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(message);
            }
        }
        public static async Task SendMail(List<string> mails, string Subject, string Body)
        {
            var message = new MailMessage();
            mails.ForEach(mail => message.To.Add(new MailAddress(mail)));
            message.From = new MailAddress(WebConfigurationManager.AppSettings["AdminUser"]);
            message.Subject = Subject;
            message.Body = Body;
            message.IsBodyHtml = true;
            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential()
                {
                    UserName = WebConfigurationManager.AppSettings["AdminUser"],
                    Password = WebConfigurationManager.AppSettings["AdminPassword"]
                };
                smtp.Credentials = credential;
                smtp.Host = WebConfigurationManager.AppSettings["SMTPName"];
                smtp.Port = int.Parse(WebConfigurationManager.AppSettings["SMTPPort"]);
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(message);
            }
        }
    }
}