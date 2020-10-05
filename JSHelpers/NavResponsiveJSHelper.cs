using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BlazorUIComponents.interfaces;
using Microsoft.JSInterop;

namespace BlazorUIComponents.JSHelpers
{
    class NavResponsiveJSHelper : JSHelper<INavResponsiveComponent, NavResponsiveJSHelper>
    {
        public NavResponsiveJSHelper(INavResponsiveComponent cmp, IJSRuntime jsRuntime)
        {
            Host = cmp;
            ObjRef = DotNetObjectReference.Create(this);
            _jsRuntime = jsRuntime;
        }
        public override async void Init()
        {
            await _jsRuntime.InvokeVoidAsync("navBarResponsiveObserverInit", Host.GetLinks(), ObjRef);
        }
        public async Task ScrollIntoView(string id)
        {
            await _jsRuntime.InvokeVoidAsync("scrollSectionIntoView", id);
        }
        [JSInvokable]
        public void SetSectionInView(string section)
        {
            Host.SetSectionInView(section);
        }
    }
}
