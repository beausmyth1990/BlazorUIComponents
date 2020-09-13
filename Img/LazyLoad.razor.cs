using System;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace BlazorUIComponents.Img
{
    public partial class LazyLoad : IDisposable
    {
        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        bool _isInView;

        JSHelper _jsHelper;

        ElementReference _imgRef;

        [Parameter]
        public string LazyLoadSrc { get; set; }

        [Parameter]
        public string PlaceHolderSrc { get; set; } = @"_content/BlazorUIComponents/img/background.png";

        [Parameter]
        public string Class { get; set; } = "lazy-img";

        [Parameter]
        public string ClassInView { get; set; } = "in-view";

        protected override void OnInitialized()
        {
            _jsHelper = new JSHelper(
                async () =>
                { 
                    _isInView = true; await InvokeAsync(() => StateHasChanged()); 
                }
                ,
                JSRuntime
                );
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await _jsHelper.CallVoidJSFunction("observeElement", _imgRef, _jsHelper.ObjRef);
            }
        }
        public void Dispose()
        {
            _jsHelper.Dispose();
        }
    }
}
