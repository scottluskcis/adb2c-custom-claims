using System.Threading.Tasks;
using Contoso.AADB2C.API.Models;

namespace Contoso.AADB2C.API.Services
{
    public interface IIdentityService
    {
        Task<OutputClaimsModel> SignUpAsync(InputClaimsModel model);
        Task<OutputClaimsModel> SignInAsync(InputClaimsModel model);
    }
}