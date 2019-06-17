using Models.Domain;

namespace Services
{
    public interface ITokenService
    {
        InviteContributor ConfirmContributor(string token);   
    }
}