using System;

namespace Api.Models;

public class GoogleRecaptchaResponse
{
    public bool success { get; set; }
    public DateTime challenge_ts { get; set; }
    public float score { get; set; }
    public string hostname { get; set; } = string.Empty;
    public string[]? ErrorCodes { get; set; }
}
