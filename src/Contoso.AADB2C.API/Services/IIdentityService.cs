using System.Threading.Tasks;
using Contoso.AADB2C.API.Models;
using Microsoft.AspNetCore.Http;

namespace Contoso.AADB2C.API.Services
{
    public interface IIdentityService
    {
        Task<OutputClaimsModel> ProcessAsync(HttpRequest request);
        Task<InputClaimsModel> ParseInputClaimsAsync(HttpRequest request);
        Task<OutputClaimsModel> SetOutputClaimsAsync(InputClaimsModel inputClaims);
    }
}