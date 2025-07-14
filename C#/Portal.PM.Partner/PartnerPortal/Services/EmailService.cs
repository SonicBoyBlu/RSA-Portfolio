using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace Acme.Services
{
    public class EmailService
    {
        private string _apiKey;
        private SendGridClient _client;
        private string _fromEmail;

        public EmailService()
        {
            _fromEmail = ConfigurationManager.AppSettings["SendGridDefaultFrom"].ToStringOrDefault();
            _apiKey = ConfigurationManager.AppSettings["SendGridApiKey"].ToStringOrDefault();
            _client = new SendGridClient(_apiKey);
        }


        public async Task Send(string emailTo, string emailSubject, string emailBody, List<string> marketingCategories = null)
        {
            var from = new EmailAddress(_fromEmail, "Acme House Co");
            var subject = "[PartnerPortal] " + emailSubject;
            var to = new EmailAddress(emailTo.ToStringOrDefault().ToLower().Trim());
            var plainTextContent = emailBody;
            // var htmlContent = emailBody;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent,null);

            if (marketingCategories.IsNotNullOrEmpty())
                msg.Categories = marketingCategories.ConvertAll(d => d.ToLower());
            
            var response = await _client.SendEmailAsync(msg);
        }

        public async Task SendUsingDynamicTemplateData(string emailTo, object dynamicTemplateData, List<string> marketingCategories = null)
        {
            var from = new EmailAddress(_fromEmail);
            var to = new EmailAddress(emailTo.ToStringOrDefault().ToLower().Trim());
            var msg = MailHelper.CreateSingleTemplateEmail(from,to, dynamicTemplateData.GetProperty("templateId").GetValue(dynamicTemplateData).ToStringOrDefault(),dynamicTemplateData);

            if (marketingCategories.IsNotNullOrEmpty())
                msg.Categories = marketingCategories.ConvertAll(d => d.ToLower());

            var response = await _client.SendEmailAsync(msg);
        }

    }
}