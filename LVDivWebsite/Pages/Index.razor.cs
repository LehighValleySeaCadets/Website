using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace LVDivWebsite.Pages;

public partial class Index : ComponentBase
{
    const string carouselName = "mainCarousel";

    [Inject]
    public IJSRuntime? JSRuntime { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        _ = JSRuntime ?? throw new ArgumentNullException(nameof(JSRuntime));

        object[] args = { carouselName };
        await JSRuntime.InvokeVoidAsync("startCarousel", args);
    }
}
