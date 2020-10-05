using System;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Web;
using BlazorUIComponents.interfaces;
using BlazorUIComponents.JSHelpers;

namespace BlazorUIComponents.Img
{
    public partial class LazyLoad : IDisposable, ILazyLoadComponent
    {
        [Inject] public IJSRuntime JSRuntime { get; set; }

        bool _inView;
        bool _isLoaded;
        
        LazyLoadJSHelper _jsHelper;

        ElementReference _imgRef;
        [Parameter] public string LazyLoadSrc { get; set; }
        [Parameter] public string PlaceHolderSrc { get; set; }
        [Parameter] public string Id { get; set; }
        [Parameter] public string Class { get; set; } = "lazy-img-container";
        [Parameter] public string ClassInView { get; set; } = "in-view";
        [Parameter] public RenderFragment ChildContent { get; set; }
        protected override void OnInitialized()
        {
            _jsHelper = new LazyLoadJSHelper(this, JSRuntime);
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await _jsHelper.ObserveElement(_imgRef);
            }
        }
        void OnLoad(ProgressEventArgs args)
        {
            _isLoaded = true;
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
