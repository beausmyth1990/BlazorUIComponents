using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace BlazorUIComponents
{
    internal class JSHelper : IDisposable
    {
        protected IJSRuntime _jsRuntime;

        public DotNetObjectReference<JSHelper> ObjRef { get; private set; }

        Action _action;

        public JSHelper(Action action, IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
            _action = action;
            ObjRef = DotNetObjectReference.Create(this);
        }

        public async Task CallVoidJSFunction(string func, params object[] args)
        {
            await _jsRuntime.InvokeVoidAsync(func, args);
        }
        
        [JSInvokable]
        public void PerformAction()
        {
            _action?.Invoke();
        }

        public void Dispose()
        {
            ObjRef.Dispose();
        }

    }
}
