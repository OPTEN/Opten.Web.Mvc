using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Opten.Web.Mvc
{
	/// <summary>
	/// The HTML Bootstrap Validation Helper.
	/// </summary>
	public static class BootstrapValidationHelper
	{

		//TODO: Do it like MVC5 with the TagBuilder/TagHelper/IGenerator

		/// <summary>
		/// The Bootstrap Validation Class.
		/// </summary>
		public static string BootstrapValidationClass = "alert alert-danger";

		/// <summary>
		/// Displays the error in bootstrap design.
		/// </summary>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <param name="modelName">Name of the model.</param>
		/// <returns></returns>
		public static IHtmlString BootstrapValidationMessage(
			this HtmlHelper htmlHelper,
			string modelName)
		{
			return BootstrapValidationMessage(
				htmlHelper: htmlHelper,
				modelName: modelName,
				htmlAttributes: null);
		}

		/// <summary>
		/// Displays the error in bootstrap design.
		/// </summary>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <param name="modelName">Name of the model.</param>
		/// <param name="htmlAttributes">The HTML attributes.</param>
		/// <returns></returns>
		public static IHtmlString BootstrapValidationMessage(
			this HtmlHelper htmlHelper,
			string modelName,
			object htmlAttributes)
		{
			return htmlHelper.ValidationMessage(
				modelName: modelName,
				htmlAttributes: GetValidationAttributes(htmlAttributes: htmlAttributes));
		}

		/// <summary>
		/// Displays the error in bootstrap design.
		/// </summary>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <param name="expression">The expression.</param>
		/// <returns></returns>
		public static IHtmlString BootstrapValidationMessageFor<TModel, TProperty>(
			this HtmlHelper<TModel> htmlHelper,
			Expression<Func<TModel, TProperty>> expression)
		{
			return BootstrapValidationMessageFor<TModel, TProperty>(
				htmlHelper: htmlHelper,
				expression: expression,
				htmlAttributes: null);
		}

		/// <summary>
		/// Displays the error in bootstrap design.
		/// </summary>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <param name="expression">The expression.</param>
		/// <param name="htmlAttributes">The HTML attributes.</param>
		/// <returns></returns>
		public static IHtmlString BootstrapValidationMessageFor<TModel, TProperty>(
			this HtmlHelper<TModel> htmlHelper,
			Expression<Func<TModel, TProperty>> expression,
			object htmlAttributes)
		{
			return htmlHelper.ValidationMessageFor(
				expression: expression,
				validationMessage: string.Empty, // if string.Empty default will be taken
				htmlAttributes: GetValidationAttributes(htmlAttributes: htmlAttributes));
		}

		/// <summary>
		/// Displays the errors in bootstrap design.
		/// </summary>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <param name="excludePropertyErrors">if set to <c>true</c> [exclude property errors].</param>
		/// <returns></returns>
		public static IHtmlString BootstrapValidationSummary(
			this HtmlHelper htmlHelper,
			bool excludePropertyErrors = true)
		{
			return BootstrapValidationSummary(
				htmlHelper: htmlHelper,
				excludePropertyErrors: excludePropertyErrors,
				htmlAttributes: null);
		}

		/// <summary>
		/// Displays the errors in bootstrap design.
		/// </summary>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <param name="excludePropertyErrors">if set to <c>true</c> [exclude property errors].</param>
		/// <param name="htmlAttributes">The HTML attributes.</param>
		/// <returns></returns>
		public static IHtmlString BootstrapValidationSummary(
			this HtmlHelper htmlHelper,
			bool excludePropertyErrors,
			object htmlAttributes)
		{
			// Nothing to do if there aren't any errors and property errors are exluded
			if (excludePropertyErrors &&
				htmlHelper.ViewData.ModelState.IsValid) return MvcHtmlString.Empty;

			TagBuilder div = new TagBuilder("div");

			// This attribute will be hit by jquery.validation and shown asynchronousely
			div.Attributes.Add("data-valmsg-summary", "true");

			if (htmlAttributes != null)
			{
				div.MergeAttributes(
					attributes: HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes: htmlAttributes));
			}

			// Add valid class otherwise it'll be shown.
			div.MergeAttribute("class", BootstrapValidationHelper.BootstrapValidationClass + " " + HtmlHelper.ValidationSummaryValidCssClassName, true);

			IEnumerable<ModelError> errors = Enumerable.Empty<ModelError>();
			if (htmlHelper.ViewData.ModelState.IsValid == false)
			{
				// Get errors from model state
				errors = (from o in htmlHelper.ViewData.ModelState.Values
						  from e in o.Errors
						  where excludePropertyErrors == false || o.Value == null
						  where o.Errors.Count > 0
						  where string.IsNullOrWhiteSpace(e.ErrorMessage) == false
						  select e);
			}

			if (errors != null && errors.Any())
			{
				// Set new class to show the errors in the correct style!
				div.MergeAttribute("class", BootstrapValidationHelper.BootstrapValidationClass + " " + HtmlHelper.ValidationSummaryCssClassName, true);

				if (errors.Count() == 1)
				{
					div.InnerHtml += errors.First().ErrorMessage;

					// When we use jquery.validation we need an <ul>!
					div.InnerHtml += "<ul style=\"display: none;\"><li></li></ul>";
				}
				else
				{
					div.InnerHtml += "<ul>";
					foreach (ModelError error in errors)
					{
						div.InnerHtml += "<li>" + error.ErrorMessage + "</li>";
					}
					div.InnerHtml += "</ul>";
				}
			}
			else
			{
				// When we use jquery.validation we need an <ul>!
				div.InnerHtml += "<ul><li style=\"display: none;\"></li></ul>";
			}

			return new HtmlString(div.ToString(TagRenderMode.Normal));
		}

		#region Private helpers

		private static RouteValueDictionary GetValidationAttributes(object htmlAttributes)
		{
			RouteValueDictionary attributes = new RouteValueDictionary();
			if (htmlAttributes != null)
			{
				attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes: htmlAttributes);
			}

			attributes["class"] = BootstrapValidationHelper.BootstrapValidationClass;

			return attributes;
		}

		#endregion

	}
}