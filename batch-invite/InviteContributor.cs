
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Domain
{
    public class InviteContributor
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int Contributor { get; set; }
        public int ContributionTypeId { get; set; }

    }
}
