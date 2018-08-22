using Opten.Common.Extensions;
using System;
using System.Web.Mvc;

namespace Opten.Web.Mvc
{
	/// <summary>
	/// The MVC Url Helper.
	/// </summary>
	public static class UrlHelperExtensions
	{

		/// <summary>
		/// Gets the current url.
		/// </summary>
		/// <param name="urlHelper">The URL helper.</param>
		/// <param name="withQuery">if set to <c>true</c> [with query].</param>
		/// <param name="withDomain">if set to <c>true</c> [with domain].</param>
		/// <returns></returns>
		public static string Current(this UrlHelper urlHelper, bool withQuery = true, bool withDomain = true)
		{
			return urlHelper.Current().GetUrl(withQuery, withDomain);
		}

		/// <summary>
		/// Gets the current url.
		/// </summary>
		/// <param name="urlHelper">The URL helper.</param>
		/// <returns></returns>
		public static Uri Current(this UrlHelper urlHelper)
		{
			return urlHelper.RequestContext.HttpContext.Request.Url;
		}

		/// <summary>
		/// Gets the base url.
		/// </summary>
		/// <param name="urlHelper">The URL helper.</param>
		/// <returns></returns>
		public static string BaseUrl(this UrlHelper urlHelper)
		{
			return urlHelper.RequestContext.HttpContext.Request.Url.GetBaseUrl();
		}

	}
}