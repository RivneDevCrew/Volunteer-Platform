using System;
using System.Configuration;
using System.Net.Mail;

namespace VolunteerSystem
{
    public class GmailService : SmtpClient
    {
        public string UserName { get; set; }

        public GmailService() :
            base( ConfigurationManager.AppSettings["GmailHost"], Int32.Parse(ConfigurationManager.AppSettings["GmailPort"]) )
        {
            this.UserName = ConfigurationManager.AppSettings["GmailUserName"];
            this.EnableSsl = Boolean.Parse(ConfigurationManager.AppSettings["GmailSsl"]);
            this.UseDefaultCredentials = false;
            this.Credentials = new System.Net.NetworkCredential(this.UserName, 
                ConfigurationManager.AppSettings["GmailPassword"]);
        }
    }
}