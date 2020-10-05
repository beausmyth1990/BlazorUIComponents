using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BlazorUIComponents.interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorUIComponents.JSHelpers
{
    internal class LazyLoadJSHelper : JSHelper<ILazyLoadComponent, LazyLoadJSHelper>
    {
        public LazyLoadJSHelper(ILazyLoadComponent cmp, IJSRuntime jsRuntime)
        {
            Host = cmp;
            ObjRef = DotNetObjectReference.Create(this);
            _jsRuntime = jsRuntime;
        }
        public override void Init() { } // Nothing to do here

        [JSInvokable]
        public void SetInView()
        {
            Host.SetInView();
        }
        public async Task ObserveElement(ElementReference element)
        {
            await _jsRuntime.InvokeVoidAsync("observeElement", element, ObjRef);
        }
    }
}
