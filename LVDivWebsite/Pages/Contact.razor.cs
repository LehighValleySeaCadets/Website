using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace LVDivWebsite.Pages;

public partial class Contact : ComponentBase
{
    private string? _token;
    private Modal? _modalRef;
    private bool _showSubmitText;
    private bool _showSuccessText;

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

        _showSubmitText = true;
        _showSuccessText = false;

        await _modalRef!.Show();
        _token = await JSRuntime.InvokeAsync<string>("getResponse");
        var response = await Client.GetAsync($"api/contact/{_token}");

        _showSubmitText = false;
        _showSuccessText = true;
        await Task.Delay(2000);
        await _modalRef!.Hide();
    }
}
