using System.Threading.Tasks;
using Models.Domain;
using Models.Requests.Emails;

namespace Services
{
    public interface IEmailService
    {
        Task InviteContributor(InviteContributorRequest model);
    }
}