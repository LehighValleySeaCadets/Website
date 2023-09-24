using SeaCadetsWebAPI.Models;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SeaCadetsWebAPI.Services;

public class RecaptchaService : IRecaptchaService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;
    private readonly ILogger<RecaptchaService> _logger;

    public RecaptchaService(IHttpClientFactory factory, IConfiguration config, ILogger<RecaptchaService> logger)
    {
        _httpClient = factory.CreateClient("recaptchaClient");
        _config = config;
        _logger = logger;
    }
    public async Task<GoogleRecaptchaResponse?> ValidateUserToken(string token)
    {
        var secret = _config["Recaptcha:SecretKey"];

        // var url = $"https://www.google.com/recaptcha/api/siteverify?secret={secret}&response={token}";

        using (_httpClient)
        {
            var httpResult = await _httpClient.GetAsync($"/recaptcha/api/siteverify?secret={secret}&response={token}");
            var response = await httpResult.Content.ReadAsStringAsync();
            
            var googleResult = JsonSerializer.Deserialize<GoogleRecaptchaResponse>(response);
            _logger.LogInformation("Returning Google Result: Success: {success}\n\tTimestamp: {time}\n\tScore: {score}\n\tHost: {host}", googleResult.success, googleResult.challenge_ts, googleResult.score, googleResult.hostname);
            return googleResult;
        }
    }
}
