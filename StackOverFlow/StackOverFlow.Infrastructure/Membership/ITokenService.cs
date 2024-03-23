
using System.Security.Claims;

namespace StackOverFlow.Infrastructure
{
    public interface ITokenService
    {
        Task<string> GetJwtToken(IList<Claim> claims, string key, string issuer, string audience);
    }
}