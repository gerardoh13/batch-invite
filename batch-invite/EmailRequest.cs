using Models.Requests.Emails;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sabio.Models.Requests
{
    public class EmailRequest : EmailBase
    {
        public string From { get; set; }
     
        public string Subject { get; set; }
    
        public string PlainTextContent { get; set; }

        public string HtmlContent { get; set; }
    }
}
