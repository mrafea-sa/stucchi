using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.IO;
using System.Configuration;
using System.Net.Configuration;
using System.Web.Configuration;
using System.Net.Mail;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.Mail;

namespace _Core.Helpers
{
    public class EmailEngine
    {
        private string _from;
        private string _filePath;
        IDictionary<string, string> _mergingFields;
        IDictionary<string, IList<IDictionary<string, string>>> _repetitiveFields;
        IList<string> _receivers;
        string _ccReceiver;
        private string _subject;

        public EmailEngine(string from, string filePath, string subject)
        {
            _from = from;
            _filePath = filePath;
            _mergingFields = new Dictionary<string, string>();
            _receivers = new List<string>();
            _repetitiveFields = new Dictionary<string, IList<IDictionary<string, string>>>();
            _subject = subject;
        }

        public IDictionary<string, string> MergingFields
        {
            set { _mergingFields = value; }
        }

        public IDictionary<string, IList<IDictionary<string, string>>> RepetitiveFields
        {
            set { _repetitiveFields = value; }
            get { return _repetitiveFields; }
        }

        public IList<string> Receivers
        {
            set { _receivers = value; }
        }

        public void AddReceivers(string receiver)
        {
            _receivers.Add(receiver);
        }

        public void AddAdminReceivers()
        {
            string[] emails = System.Configuration.ConfigurationManager.AppSettings["EmailsReceiver"].Split(';');
            foreach (string email in emails) AddReceivers(email);
        }
        public void AddCCReceiver(string receiver)
        {
            _ccReceiver = receiver;
        }


        public void AddMergingField(string property, string value)
        {
            _mergingFields.Add(property, value);
        }

        public string GetMergedContent()
        {
            string emailBodyContent = string.Empty;

            HttpRequest Request = HttpContext.Current.Request;

            using (StreamReader strReader = new StreamReader(HttpContext.Current.Request.MapPath(string.Concat(System.Configuration.ConfigurationManager.AppSettings["EmailTemplates"], _filePath, ".html"))))
            {
                //merge the template file
                emailBodyContent = strReader.ReadToEnd();

                foreach (string property in _mergingFields.Keys)
                    emailBodyContent = emailBodyContent.Replace(string.Format("${0}$", property), _mergingFields[property]);

                foreach (string key in _repetitiveFields.Keys)
                {
                    using (StreamReader strRepReader = new StreamReader(HttpContext.Current.Request.MapPath(string.Concat(System.Configuration.ConfigurationManager.AppSettings["EmailTemplates"], key))))
                    {
                        string content = string.Empty;
                        string fileContent = strRepReader.ReadToEnd();
                        foreach (IDictionary<string, string> matches in _repetitiveFields[key])
                        {
                            string repetitiveContent = fileContent;
                            foreach (string keyFields in matches.Keys)
                            {
                                repetitiveContent = repetitiveContent.Replace(string.Format("${0}$", keyFields), matches[keyFields]);
                            }
                            content += repetitiveContent;
                        }
                        emailBodyContent = emailBodyContent.Replace(string.Format("${0}$", key), content);
                    }
                }
            }

            return emailBodyContent;
        }

        public static string GetEmailContent(string filePathName)
        {
            string emailContent = string.Empty;
            HttpRequest Request = HttpContext.Current.Request;

            using (StreamReader strReader = new StreamReader(HttpContext.Current.Request.MapPath(filePathName)))
            {
                //merge the template file
                emailContent = strReader.ReadToEnd();
            }

            return emailContent;
        }

        public void SendEmail()
        {
            string emailContent = CreateMainTemplateContent();
            string emailBodyContent = GetMergedContent();

            emailContent = emailContent.Replace("$body$", emailBodyContent);
            foreach (string account in _receivers)
                SendEmail(_from, account, _ccReceiver, _subject, emailContent);
        }

        /// <summary>
        /// Sends the email to the address specififed in web.config
        /// </summary>
        /// <param name="content">Content to be sent.</param>
        public static void SendEmail(string from, string to, string ccreceiver, string subject, string content)
        {
            try
            {
                from = string.IsNullOrEmpty(from) ? System.Configuration.ConfigurationManager.AppSettings["EmailsFrom"] : from;
                MailMessage message = new MailMessage(from, to, subject, content);
                message.IsBodyHtml = true;
                if (!string.IsNullOrEmpty(ccreceiver))
                {
                    message.CC.Add(ccreceiver);
                }
                EmailSender.Get().Send(message);
            }
            catch (Exception ex)
            {
                //TODO
            }

        }

        private string CreateMainTemplateContent()
        {
            string emailContent = GetEmailContent(string.Concat(System.Configuration.ConfigurationManager.AppSettings["EmailTemplates"], "MainTemplate.html"));
            emailContent = emailContent.Replace("$imagesUrl$", string.Concat(System.Configuration.ConfigurationManager.AppSettings["Host"], System.Configuration.ConfigurationManager.AppSettings["EmailImages"]));

            return emailContent;
        }
    }
}