using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Requests.Emails
{
   public  class InviteContributorRequest : EmailRequest
    {

        public int UserId { get; set; }
        public int ContributionTypeId { get; set; }
        public string ContributionType  { get; set; }
        public int EventId { get; set; }
        public int TokenType { get; set; }
        public string Token { get; set; }
    }
}
