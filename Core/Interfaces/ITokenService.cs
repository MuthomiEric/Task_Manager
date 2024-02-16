using Core.Entities;

namespace Core.Interfaces
{
    public interface ITokenService
    {
        (string expiresin, string token) CreateToken(SystemUser user, IList<string>? roles = null);
    }
}
