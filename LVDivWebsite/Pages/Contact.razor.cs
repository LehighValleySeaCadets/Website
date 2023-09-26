using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace LVDivWebsite.Pages;

public partial class Contact : ComponentBase
{
    private string? _token;

    [Inject]
    private IConfiguration Config {  get; set; }
    [Inject]
    private IJSRuntime? JSRuntime { get; set; }

    protected async Task Submit()
    {
        _ = JSRuntime ?? throw new NullReferenceException();
        _ = Config ?? throw new NullReferenceException();

        _token = await JSRuntime.InvokeAsync<string>("getResponse");

        // TODO:  Create the http client in Program.cs and use DI
        var client = new HttpClient
        {
            BaseAddress = new Uri(Config["Api:Url"])
        };

        // TODO:  Do something with the response
        var response = await client.GetAsync($"api/contact/{_token}");
    }
}
