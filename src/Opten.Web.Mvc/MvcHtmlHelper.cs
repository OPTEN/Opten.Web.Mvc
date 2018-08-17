using System;
using System.Web;
using System.Web.Mvc;

namespace Opten.Web.Mvc
{
	/// <summary>
	/// The MVC HTML Helper.
	/// </summary>
	public static class MvcHtmlHelper
	{

#pragma warning disable 1591

		public static IHtmlString If(this HtmlHelper htmlHelper, bool test, string valueIfTrue, string valueIfFalse)
		{
			return test ? new HtmlString(valueIfTrue) : new HtmlString(valueIfFalse);
		}

		public static IHtmlString If(this HtmlHelper htmlHelper, bool test, Func<string> valueIfTrue, Func<string> valueIfFalse)
		{
			return test ? new HtmlString(valueIfTrue()) : new HtmlString(valueIfFalse());
		}

		public static IHtmlString If(this HtmlHelper htmlHelper, bool test, string valueIfTrue)
		{
			return test ? new HtmlString(valueIfTrue) : MvcHtmlString.Empty;
		}

		public static IHtmlString If(this HtmlHelper htmlHelper, bool test, Func<string> valueIfTrue)
		{
			return test ? new HtmlString(valueIfTrue()) : MvcHtmlString.Empty;
		}

#pragma warning restore 1591

		/// <summary>
		/// Check against the executing action.
		/// </summary>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <param name="actionName">Name of the action.</param>
		/// <param name="controllerName">Name of the controller.</param>
		/// <returns></returns>
		public static bool IsAction(this HtmlHelper htmlHelper, string actionName, string controllerName)
		{
			string currentAction = htmlHelper.ViewContext.RouteData.GetRequiredString("action");
			string currentController = htmlHelper.ViewContext.RouteData.GetRequiredString("controller");

			return actionName.Equals(currentAction, StringComparison.OrdinalIgnoreCase) &&
				controllerName.Equals(currentController, StringComparison.OrdinalIgnoreCase);
		}

		/// <summary>
		/// Check against the executing controller.
		/// </summary>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <param name="controllerName">Name of the controller.</param>
		/// <returns></returns>
		public static bool IsController(this HtmlHelper htmlHelper, string controllerName)
		{
			string currentController = htmlHelper.ViewContext.RouteData.GetRequiredString("controller");

			return controllerName.Equals(currentController, StringComparison.OrdinalIgnoreCase);
		}

	}
}