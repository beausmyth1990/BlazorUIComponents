using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace BlazorUIComponents
{
    abstract internal class JSHelper<T, U> : IDisposable
        where T : class
        where U : JSHelper<T, U>
    {
        protected IJSRuntime _jsRuntime;
        public DotNetObjectReference<U> ObjRef { get; protected set; }
        public T Host { get; set; }
        abstract public void Init();
        public void Dispose()
        {
            ObjRef.Dispose();
        }
    }
}
