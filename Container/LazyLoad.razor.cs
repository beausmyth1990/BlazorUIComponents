using System;
using BlazorUIComponents.interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;
using BlazorUIComponents.JSHelpers;

namespace BlazorUIComponents.Container
{
    partial class LazyLoad : IDisposable, ILazyLoadComponent
    {
        LazyLoadJSHelper _jsHelper;

        bool _inView;

        ElementReference _containerRef;
        [Inject] public IJSRuntime JSRuntime { get; set; }
        [Parameter] public string Id { get; set; }
        [Parameter] public string Class { get; set; } = "d-sm-flex align-items-center lazy-container";
        [Parameter] public string ClassInView { get; set; } = "in-view";
        [Parameter] public RenderFragment ChildContent { get; set; }
        

        protected override void OnInitialized()
        {
            base.OnInitialized();
            _jsHelper = new LazyLoadJSHelper(this, JSRuntime);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await _jsHelper.ObserveElement(_containerRef);
            }
        }
        public void SetInView()
        {
            _inView = true;
            InvokeAsync(() => StateHasChanged());
        }
        public void Dispose()
        {
            _jsHelper.Dispose();
        }
    }
}
