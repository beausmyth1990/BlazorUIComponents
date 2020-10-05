using BlazorUIComponents.nav;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlazorUIComponents.interfaces
{
    public interface INavResponsiveComponent
    {
        Task SetSectionInView(string section);
        IReadOnlyList<NavLinkModel> GetLinks();
    }
}
