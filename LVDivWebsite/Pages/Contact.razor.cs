using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace LVDivWebsite.Pages;

public partial class Contact : ComponentBase
{
    private string? _token;
    private Modal? _modalRef;
    private string _modalBodyText = "Validating form data.";

    [Inject]
    private HttpClient? Client { get; set; }
    [Inject]
    private IConfiguration? Config {  get; set; }
    [Inject]
    private IJSRuntime? JSRuntime { get; set; }

    protected async Task Submit()
    {
        _ = Config ?? throw new NullReferenceException();
        _ = Client ?? throw new NullReferenceException();
        _ = JSRuntime ?? throw new NullReferenceException();

        await _modalRef!.Show();
        _token = await JSRuntime.InvokeAsync<string>("getResponse");

        // TODO:  Create the http client in Program.cs and use DI
        // var client = new HttpClient();

        // TODO:  Do something with the response
        var response = await Client.GetAsync($"api/contact/{_token}");
        _modalBodyText = "Your form has been successfully submitted.";
        await _modalRef!.Hide();
    }
}
