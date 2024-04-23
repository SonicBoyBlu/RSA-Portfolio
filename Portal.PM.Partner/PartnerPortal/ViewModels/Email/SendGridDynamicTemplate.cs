using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Acme.ViewModels.Email
{
    /// <summary>
    /// SendGrid dynamic-templates Prop LOWER CASE ONLY
    /// </summary>
    public class SendGridDynamicTemplate
    {
        public class ForgotPassword
        {
            public string templateId { get; set; }
            public string to { get; set; }
            public string resetpasswordurl { get; set; }

            public ForgotPassword()
            {
                this.templateId = "d-1dafc7842c8e4aadb8f20a8e728519b9";
            }

        }

        public class PasswordChangedNotice
        {
            public string templateId { get; set; }
            public string to { get; set; }
            
            public PasswordChangedNotice()
            {
                this.templateId = "d-6766502ad82a4d8f8fdd2b90313a5469";
            }

        }
    }


}