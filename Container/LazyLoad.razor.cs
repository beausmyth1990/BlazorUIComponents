using System;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace BlazorUIComponents.Container
{
    partial class LazyLoad
    {
        bool _inView;

        ElementReference _containerRef;

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        JSHelper _jsHelper;

        [Parameter]
        public string Class { get; set; } = "lazy-container";

        [Parameter]
        public string ClassInView { get; set; } = "in-view";

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            _jsHelper = new JSHelper(() => { _inView = true; InvokeAsync(() => StateHasChanged()); }, JSRuntime);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await _jsHelper.CallVoidJSFunction("observeElement", _containerRef, _jsHelper.ObjRef);
            }
        }
        public void Dispose()
        {
            _jsHelper.Dispose();
        }
    }
}
