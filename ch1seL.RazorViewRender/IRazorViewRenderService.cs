using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace ch1seL.RazorViewRender
{
    public interface IRazorViewRenderService
    {
        Task<string> RenderStringAsync<TModel>(string name, TModel model,
            IReadOnlyDictionary<string, object> tempData = null, HtmlHelperOptions htmlHelperOptions = null);
    }
}