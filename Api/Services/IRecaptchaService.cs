using Api.Models;
using System.Threading.Tasks;

namespace Api.Services;

public interface IRecaptchaService
{
    Task<GoogleRecaptchaResponse?> ValidateUserToken(string token);
}
