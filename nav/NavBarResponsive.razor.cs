using Microsoft.AspNetCore.Components;
using BlazorUIComponents.interfaces;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using BlazorUIComponents.JSHelpers;

namespace BlazorUIComponents.nav
{
    public partial class NavBarResponsive : INavResponsiveComponent
    {
        bool _inView;
        string _sectionInView;
        NavResponsiveJSHelper _jsHelper { get; set; }
        [Inject] public IJSRuntime JSRuntime { get; set; }
        [Parameter] public bool SamePageNav { get; set; }
        [Parameter] public string Class { get; set; } = "navbar-res";
        [Parameter] public string SectionInViewClass { get; set; } = "section-in-view";
        [Parameter] public IReadOnlyList<NavLinkModel> Links { get; set; }
        [Parameter] public RenderFragment<NavLinkModel> LinkTemplate { get; set; }
        [Parameter] public RenderFragment ToggleContent { get; set; }
        protected override void OnInitialized()
        {
            _jsHelper = new NavResponsiveJSHelper(this, JSRuntime);
        }
        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                if(SamePageNav)
                    _jsHelper.Init();
            }
        }
        void OnToggle()
        {
            _inView = !_inView;
        }
        public IReadOnlyList<NavLinkModel> GetLinks() => Links;
        public async Task SetSectionInView(string section)
        {
            _sectionInView = section;
            await InvokeAsync(() => StateHasChanged());

        }
        public async Task ScrollSectionIntoView(string id)
        {
            await _jsHelper.ScrollIntoView(id);
            await InvokeAsync(() => StateHasChanged());
        }
    }
    /* At this point this class only has a use within this namespace */
    public class NavLinkModel
    {
        public NavLinkModel(string _class, string href, string title)
        {
            Class = _class;
            Href = href;
            Title = title;
        }
        public string Class { get; set; }
        public string Href { get; set; }
        public string Title { get; set; }
    }
}
