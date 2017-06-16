using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Opten.Web.Mvc
{
	/// <summary>
	/// The HTML Bootstrap Input Helper.
	/// </summary>
	public static class BootstrapInputHelper
	{

		/// <summary>
		/// Renders the textbox in bootstrap design.
		/// </summary>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <param name="expression">The expression.</param>
		/// <returns></returns>
		public static MvcHtmlString BootstrapTextBoxFor<TModel, TProperty>(
			this HtmlHelper<TModel> htmlHelper,
			Expression<Func<TModel, TProperty>> expression)
		{
			return htmlHelper.BootstrapTextBoxFor(
				expression: expression,
				format: null,
				htmlAttributes: null);
		}

		/// <summary>
		/// Renders the textbox in bootstrap design.
		/// </summary>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <param name="expression">The expression.</param>
		/// <param name="format">The format.</param>
		/// <returns></returns>
		public static MvcHtmlString BootstrapTextBoxFor<TModel, TProperty>(
			this HtmlHelper<TModel> htmlHelper,
			Expression<Func<TModel, TProperty>> expression,
			string format)
		{
			return htmlHelper.BootstrapTextBoxFor(
				expression: expression,
				format: format,
				htmlAttributes: null);
		}

		/// <summary>
		/// Renders the textbox in bootstrap design.
		/// </summary>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <param name="expression">The expression.</param>
		/// <param name="htmlAttributes">The HTML attributes.</param>
		/// <returns></returns>
		public static MvcHtmlString BootstrapTextBoxFor<TModel, TProperty>(
			this HtmlHelper<TModel> htmlHelper,
			Expression<Func<TModel, TProperty>> expression,
			object htmlAttributes)
		{
			return htmlHelper.BootstrapTextBoxFor(
				expression: expression,
				format: null,
				htmlAttributes: htmlAttributes);
		}

		/// <summary>
		/// Renders the textbox in bootstrap design.
		/// </summary>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <param name="expression">The expression.</param>
		/// <param name="format">The format.</param>
		/// <param name="htmlAttributes">The HTML attributes.</param>
		/// <returns></returns>
		public static MvcHtmlString BootstrapTextBoxFor<TModel, TProperty>(
			this HtmlHelper<TModel> htmlHelper,
			Expression<Func<TModel, TProperty>> expression,
			string format,
			object htmlAttributes)
		{
			List<string> cssClasses = new List<string> { "form-control" };

			RouteValueDictionary attributes = new RouteValueDictionary();
			if (htmlAttributes != null)
			{
				attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
			}

			if (attributes.ContainsKey("class"))
			{
				cssClasses.AddRange(attributes["class"].ToString().Trim().Split(' '));
				attributes.Remove("class");
			}

			cssClasses = cssClasses.Distinct().ToList();

			attributes.Add("class", string.Join(" ", cssClasses).Trim());

			return htmlHelper.TextBoxFor(expression, format, attributes);
		}

		/// <summary>
		/// Renders the textarea in bootstrap design.
		/// </summary>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <param name="expression">The expression.</param>
		/// <param name="rows">The rows.</param>
		/// <returns></returns>
		public static MvcHtmlString BootstrapTextAreaFor<TModel, TProperty>(
			this HtmlHelper<TModel> htmlHelper,
			Expression<Func<TModel, TProperty>> expression,
			int rows = 4)
		{
			return htmlHelper.BootstrapTextAreaFor(
				expression: expression,
				rows: rows,
				htmlAttributes: null);
		}

		/// <summary>
		/// Renders the textarea in bootstrap design.
		/// </summary>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <param name="expression">The expression.</param>
		/// <param name="rows">The rows.</param>
		/// <param name="htmlAttributes">The HTML attributes.</param>
		/// <returns></returns>
		public static MvcHtmlString BootstrapTextAreaFor<TModel, TProperty>(
			this HtmlHelper<TModel> htmlHelper,
			Expression<Func<TModel, TProperty>> expression,
			int rows,
			object htmlAttributes)
		{
			List<string> cssClasses = new List<string> { "form-control" };

			RouteValueDictionary attributes = new RouteValueDictionary();
			if (htmlAttributes != null)
			{
				attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
			}

			if (attributes.ContainsKey("class"))
			{
				cssClasses.AddRange(attributes["class"].ToString().Trim().Split(' '));
				attributes.Remove("class");
			}

			cssClasses = cssClasses.Distinct().ToList();

			attributes.Add("class", string.Join(" ", cssClasses).Trim());

			if (rows > 0)
			{
				attributes.Add("rows", rows);
			}

			return htmlHelper.TextAreaFor(expression, attributes);
		}

		/// <summary>
		/// Renders the checkbox in bootstrap design.
		/// </summary>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <param name="expression">The expression.</param>
		/// <returns></returns>
		public static MvcHtmlString BootstrapCheckboxFor<TModel>(
			this HtmlHelper<TModel> htmlHelper,
			Expression<Func<TModel, bool>> expression)
		{
			return htmlHelper.BootstrapCheckboxFor(
				expression: expression,
				htmlAttributes: null);
		}

		/// <summary>
		/// Renders the checkbox in bootstrap design.
		/// </summary>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <param name="expression">The expression.</param>
		/// <param name="htmlAttributes">The HTML attributes.</param>
		/// <returns></returns>
		public static MvcHtmlString BootstrapCheckboxFor<TModel>(
			this HtmlHelper<TModel> htmlHelper,
			Expression<Func<TModel, bool>> expression,
			object htmlAttributes)
		{
			TagBuilder div = new TagBuilder("div");
			div.AddCssClass("checkbox");

			TagBuilder label = new TagBuilder("label");

			label.InnerHtml += htmlHelper.CheckBoxFor(expression, htmlAttributes);
			div.InnerHtml += label;

			return MvcHtmlString.Create(div.ToString(TagRenderMode.Normal));
		}

		/// <summary>
		/// Renders the radio button in bootstrap design.
		/// </summary>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <param name="expression">The expression.</param>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static MvcHtmlString BootstrapRadioButtonFor<TModel>(
			this HtmlHelper<TModel> htmlHelper,
			Expression<Func<TModel, bool>> expression,
			object value)
		{
			return htmlHelper.BootstrapRadioButtonFor(
				expression: expression,
				value: value,
				htmlAttributes: null);
		}

		/// <summary>
		/// Renders the radio button in bootstrap design.
		/// </summary>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <param name="expression">The expression.</param>
		/// <param name="value">The value.</param>
		/// <param name="htmlAttributes">The HTML attributes.</param>
		/// <returns></returns>
		public static MvcHtmlString BootstrapRadioButtonFor<TModel>(
			this HtmlHelper<TModel> htmlHelper,
			Expression<Func<TModel, bool>> expression,
			object value,
			object htmlAttributes)
		{
			TagBuilder div = new TagBuilder("div");
			div.AddCssClass("radio");

			TagBuilder label = new TagBuilder("label");

			label.InnerHtml += htmlHelper.RadioButtonFor(expression, value, htmlAttributes);
			div.InnerHtml += label;

			return MvcHtmlString.Create(div.ToString(TagRenderMode.Normal));
		}

	}
}