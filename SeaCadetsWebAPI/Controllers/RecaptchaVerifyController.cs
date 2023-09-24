using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SeaCadetsWebAPI.Services;

namespace SeaCadetsWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RecaptchaVerifyController : ControllerBase
{
    private readonly IRecaptchaService _recaptcha;    

    public RecaptchaVerifyController(IRecaptchaService recaptcha)
    {
        _recaptcha = recaptcha;
    }

    [HttpGet("{token}")]
    public async Task<bool> Get(string token) 
    {
        try
        {
            var response = await _recaptcha.ValidateUserToken(token) 
                ?? throw new Exception("An error occurred while verifying user identity.");

            if (response.success && response.score > 0.5f)
            {
                return true;
            }
            return false;
        }
        catch
        {
            return false;
        }
    }
}
