using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace LVDivWebsite.Pages;

public partial class Contact : ComponentBase
{
    private string? _token;

    [Inject]
    private IJSRuntime? JSRuntime { get; set; }

    protected async Task Submit()
    {
        _ = this.JSRuntime ?? throw new NullReferenceException();

        _token = await this.JSRuntime.InvokeAsync<string>("getResponse");

        // TODO:  Create the http client in Program.cs and use DI
        var client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7134")
        };

        // TODO:  Do something with the response
        var response = client.GetAsync($"/api/RecaptchaVerify/{_token}");
    }
}
