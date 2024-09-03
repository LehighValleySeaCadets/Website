using Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Api;

public class RecaptchaHttpTrigger
{
    private readonly IRecaptchaService _recaptcha;
    private readonly ILogger<RecaptchaHttpTrigger> _logger;

    public RecaptchaHttpTrigger(IRecaptchaService recaptchaService, ILogger<RecaptchaHttpTrigger> logger)
    {
        _recaptcha = recaptchaService;
        _logger = logger;
    }

    [Function("VerifyRecaptcha")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "contact/{token}")] HttpRequest req, string token)
    {
        _logger.LogInformation("Contact form has been submitted");
        _logger.LogInformation("Validating token for user.  Token: {0}", token);

        try
        {
            // Validate the token
            var response = await _recaptcha.ValidateUserToken(token)
                    ?? throw new Exception("An error occurred while validating the token.");

            _logger.LogInformation($"Google reCaptcha response: {response.success}, {response.score}, {response.challenge_ts}, {response.hostname}");
            
            // TODO:  Refactor this code so that the contact form is called first, then submit to recaptcha, then email.
            // Possibly use orchestration functions
        }
        catch (Exception ex)
        {
            return new BadRequestObjectResult(ex);
        }

        string responseMessage = string.IsNullOrEmpty(token)
            ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
            : $"Hello, {token}. This HTTP triggered function executed successfully.";

        return new OkObjectResult(responseMessage);
    }
}
