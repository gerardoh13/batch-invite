using Models.Domain;
using Models.Requests.Emails;
using System.Collections.Generic;

namespace Services
{
    public interface IContributorService
    {
        List<InviteContributorRequest> Invite_Contributors(List<InviteContributorRequest> model);
        void UpdateConfirmStatus(int userId);
        void InsertContributors(InviteContributor model);

    }
}