using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;

namespace ch1seL.RazorViewRender {
	public class RazorViewRenderService : IRazorViewRenderService {
		private readonly IServiceProvider _serviceProvider;
		private readonly ITempDataProvider _tempDataProvider;
		private readonly IRazorViewEngine _viewEngine;

		public RazorViewRenderService(IRazorViewEngine viewEngine, ITempDataProvider tempDataProvider,
		                              IServiceProvider serviceProvider) {
			_viewEngine = viewEngine;
			_tempDataProvider = tempDataProvider;
			_serviceProvider = serviceProvider;
		}

		public async Task<string> RenderStringAsync<TModel>(string name, TModel model,
		                                                    IReadOnlyDictionary<string, object> tempData = null, HtmlHelperOptions htmlHelperOptions = null) {
			var actionContext = new ActionContext(new DefaultHttpContext { RequestServices = _serviceProvider },
			                                      new RouteData(), new ActionDescriptor());

			ViewEngineResult viewEngineResult = _viewEngine.FindView(actionContext, name, false);

			if (!viewEngineResult.Success) {
				throw new InvalidOperationException($"Couldn't find view '{name}'");
			}

			var tempDataDictionary = new TempDataDictionary(actionContext.HttpContext, _tempDataProvider);
			if (tempData != null) {
				foreach ((string key, object value) in tempData) {
					tempDataDictionary.Add(key, value);
				}
			}

			IView view = viewEngineResult.View;

			await using var output = new StringWriter();

			var viewContext = new ViewContext(
				actionContext,
				view,
				new ViewDataDictionary<TModel>(new EmptyModelMetadataProvider(), new ModelStateDictionary()) {
					Model = model
				},
				tempDataDictionary
				,
				output,
				htmlHelperOptions ?? new HtmlHelperOptions());

			await view.RenderAsync(viewContext);

			return output.ToString();
		}
	}
}