using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Razor;

namespace ch1seL.RazorViewRender
{
    public class ViewLocationExpander: IViewLocationExpander {
        private readonly string _viewsPath;

        public ViewLocationExpander(string viewsPath)
        {
            _viewsPath = viewsPath;
        }

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations) {
            return viewLocations
                .Append($"/{_viewsPath}/{{1}}/{{0}}.cshtml");
        }

        public void PopulateValues(ViewLocationExpanderContext context) {
            context.Values["customviewlocation"] = nameof(ViewLocationExpander);
        }
    }
}