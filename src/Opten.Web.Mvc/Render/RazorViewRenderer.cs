using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Opten.Web.Mvc.Render
{
	/// <summary>
	/// Converts a .cshtml and model to a desired output (e.g. string).
	/// </summary>
	public class RazorViewRenderer : IRenderer
	{

		private readonly string _viewName;
		private readonly object _model;

		/// <summary>
		/// Gets or sets a value indicating whether [trim output].
		/// </summary>
		/// <value>
		///   <c>true</c> if [trim output]; otherwise, <c>false</c>.
		/// </value>
		public bool TrimOutput { get; set; } = true;

		/// <summary>
		/// Initializes a new instance of the <see cref="RazorViewRenderer"/> class.
		/// </summary>
		/// <param name="viewName">Name of the view.</param>
		/// <param name="model">The model.</param>
		/// <exception cref="ArgumentNullException">viewName</exception>
		public RazorViewRenderer(string viewName, object model)
		{
			if (ViewEngines.Engines.Any(engine => engine.GetType() == typeof(RazorViewRendererEngine)) == false)
			{
				ViewEngines.Engines.Add(new RazorViewRendererEngine());
			}

			_viewName = viewName ?? throw new ArgumentNullException(nameof(viewName));
			_model = model;
		}

		/// <summary>
		/// Renders the view as a string.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String" /> that represents this instance.
		/// </returns>
		public new string ToString()
		{
			using (Controller controller = CreateController<RazorViewRendererController>())
			{
				using (StringWriter writer = new StringWriter())
				{
					ViewDataDictionary viewData = new ViewDataDictionary(this._model);
					TempDataDictionary tempData = new TempDataDictionary();

					ViewEngineResult viewResult = ViewEngines.Engines.FindView(controller.ControllerContext, this._viewName, null);

					ViewContext viewContext = new ViewContext(controller.ControllerContext, viewResult.View, viewData, tempData, writer);
					viewResult.View.Render(viewContext, writer);

					string output = writer.GetStringBuilder().ToString();

					if (this.TrimOutput)
					{
						output = output.Trim();
					}

					return output;
				}
			}
		}

		#region Private methods

		/// <summary>
		/// Creates the controller.
		/// </summary>
		/// <typeparam name="TController">The type of the controller.</typeparam>
		/// <param name="routeData">The route data.</param>
		/// <returns></returns>
		/// <exception cref="InvalidOperationException">Can't create Controller Context if no " +
		/// 					"active HttpContext instance is available.</exception>
		private TController CreateController<TController>(RouteData routeData = null) where TController : Controller, new()
		{
			TController controller = new TController();

			HttpContextBase wrapper;
			if (HttpContext.Current != null)
			{
				wrapper = new HttpContextWrapper(HttpContext.Current);
			}
			else
			{
				throw new InvalidOperationException(
					"Can't create Controller Context if no " +
					"active HttpContext instance is available.");
			}

			if (routeData == null)
			{
				routeData = new RouteData();
			}

			// add the controller routing if not existing
			if (routeData.Values.ContainsKey("controller") == false)
			{
				string controllerName = controller.GetType().Name.Replace("Controller", string.Empty);
				routeData.Values.Add("controller", controllerName);
			}

			controller.ControllerContext = new ControllerContext(wrapper, routeData, controller);

			return controller;
		}

		private class RazorViewRendererEngine : RazorViewEngine
		{
			public RazorViewRendererEngine()
			{
				this.ViewLocationFormats = new[]
				{
					"~/Views/{1}/{0}.cshtml",
					"~/Views/Shared/{1}/{0}.cshtml",
					"~/Views/Shared/MailTemplate/{0}.cshtml",
					"~/Views/MailTemplate/{0}.cshtml",
					"~/Views/MailTemplate/{1}/{0}.cshtml"
				};
			}
		}

		private class RazorViewRendererController : Controller { }

		#endregion

	}
}