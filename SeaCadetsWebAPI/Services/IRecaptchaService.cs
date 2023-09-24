using SeaCadetsWebAPI.Models;

namespace SeaCadetsWebAPI.Services;

public interface IRecaptchaService
{
    Task<GoogleRecaptchaResponse?> ValidateUserToken(string token);
}
