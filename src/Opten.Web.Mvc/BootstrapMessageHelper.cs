using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Opten.Web.Mvc
{

	/// <summary>
	/// The Message Type
	/// </summary>
	public enum MessageType
	{
		/// <summary>
		/// Success message (green)
		/// </summary>
		Success,
		/// <summary>
		/// Danger/Error message (red)
		/// </summary>
		Danger,
		/// <summary>
		/// Info message (blue)
		/// </summary>
		Info,
		/// <summary>
		/// Warning message (yellow)
		/// </summary>
		Warning
	}

	/// <summary>
	/// The HTML Bootstrap Message Helper.
	/// </summary>
	public static class BootstrapMessageHelper
	{

		/// <summary>
		/// Adds an alert to the view or temp data.
		/// </summary>
		/// <param name="controller">The controller.</param>
		/// <param name="messageType">Type of the message.</param>
		/// <param name="message">The message.</param>
		/// <param name="showAfterRedirect">if set to <c>true</c> [show after redirect].</param>
		/// <param name="key">The filter key (only required if you want to have multiple @Html.BootstrapAlert() in one .cshtml).</param>
		public static void Alert(
			this Controller controller,
			MessageType messageType,
			string message,
			bool showAfterRedirect = false,
			string key = null)
		{
			if (showAfterRedirect)
			{
				controller.TempData.Alert(messageType, message, key);
			}
			else
			{
				controller.ViewData.Alert(messageType, message, key);
			}
		}

		/// <summary>
		/// Adds an alert to the view data.
		/// </summary>
		/// <param name="viewData">The view data.</param>
		/// <param name="messageType">Type of the message.</param>
		/// <param name="message">The message.</param>
		/// <param name="key">The filter key (only required if you want to have multiple @Html.BootstrapAlert() in one .cshtml).</param>
		public static void Alert(
			this ViewDataDictionary viewData,
			MessageType messageType,
			string message,
			string key = null)
		{
			key = MakeKey(key, messageType);
			viewData[key] = message;
		}

		/// <summary>
		/// Adds an alert to the temp data.
		/// </summary>
		/// <param name="tempData">The temporary data.</param>
		/// <param name="messageType">Type of the message.</param>
		/// <param name="message">The message.</param>
		/// <param name="key">The filter key (only required if you want to have multiple @Html.BootstrapAlert() in one .cshtml).</param>
		public static void Alert(
			this TempDataDictionary tempData,
			MessageType messageType,
			string message,
			string key = null)
		{
			key = MakeKey(key, messageType);
			tempData[key] = message;
		}

		/// <summary>
		/// Renders the message in bootstrap design.
		/// </summary>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <param name="messageType">Type of the message.</param>
		/// <param name="text">The text.</param>
		/// <param name="dismissible">if set to <c>true</c> [dismissible].</param>
		/// <returns></returns>
		public static IHtmlString BootstrapAlert(
			this HtmlHelper htmlHelper,
			MessageType messageType,
			string text,
			bool dismissible)
		{
			return htmlHelper.BootstrapAlert(
				messageType: messageType,
				text: text,
				closeText: "Close",
				dismissible: dismissible);
		}

		/// <summary>
		/// Renders the message in bootstrap design.
		/// </summary>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <param name="messageType">Type of the message.</param>
		/// <param name="text">The text.</param>
		/// <param name="closeText">The close text.</param>
		/// <param name="dismissible">if set to <c>true</c> [dismissible].</param>
		/// <returns></returns>
		public static IHtmlString BootstrapAlert(
			this HtmlHelper htmlHelper,
			MessageType messageType,
			string text,
			string closeText,
			bool dismissible)
		{
			return htmlHelper.BootstrapAlert(
				messageType: messageType,
				text: text,
				closeText: closeText,
				dismissible: dismissible,
				fade: false);
		}

		/// <summary>
		/// Renders the message in bootstrap design.
		/// </summary>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <param name="messageType">Type of the message.</param>
		/// <param name="text">The text.</param>
		/// <param name="dismissible">if set to <c>true</c> [dismissible].</param>
		/// <param name="fade">if set to <c>true</c> [fade].</param>
		/// <returns></returns>
		public static IHtmlString BootstrapAlert(
			this HtmlHelper htmlHelper,
			MessageType messageType,
			string text,
			bool dismissible,
			bool fade)
		{
			return htmlHelper.BootstrapAlert(
				messageType: messageType,
				text: text,
				closeText: "Close",
				dismissible: dismissible,
				fade: fade);
		}

		/// <summary>
		/// Renders the message in bootstrap design.
		/// </summary>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <param name="messageType">Type of the message.</param>
		/// <param name="text">The text.</param>
		/// <param name="closeText">The close text.</param>
		/// <param name="dismissible">if set to <c>true</c> [dismissible].</param>
		/// <param name="fade">if set to <c>true</c> [fade].</param>
		/// <returns></returns>
		public static IHtmlString BootstrapAlert(
			this HtmlHelper htmlHelper,
			MessageType messageType,
			string text,
			string closeText,
			bool dismissible,
			bool fade)
		{
			KeyValuePair<MessageType, string> alert = new KeyValuePair<MessageType, string>(messageType, text);

			return new HtmlString(BootstrapAlert(
				alert: alert,
				closeText: closeText,
				dismissible: dismissible,
				fade: fade).ToString(TagRenderMode.Normal));
		}

		/// <summary>
		/// Renders the messages in bootstrap design.
		/// </summary>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <param name="dismissible">if set to <c>true</c> [dismissible].</param>
		/// <param name="showOnlyWithKey">Shows only alerts with specific key (only required if you want to have multiple @Html.BootstrapAlert() in one .cshtml).</param>
		/// <returns></returns>
		public static IHtmlString BootstrapAlert(
			this HtmlHelper htmlHelper,
			bool dismissible,
			string showOnlyWithKey = null)
		{
			return htmlHelper.BootstrapAlert(
				closeText: "Close",
				dismissible: dismissible,
				showOnlyWithKey: showOnlyWithKey);
		}

		/// <summary>
		/// Renders the messages in bootstrap design.
		/// </summary>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <param name="closeText">The close text.</param>
		/// <param name="dismissible">if set to <c>true</c> [dismissible].</param>
		/// <param name="showOnlyWithKey">Shows only alerts with specific key (only required if you want to have multiple @Html.BootstrapAlert() in one .cshtml).</param>
		/// <returns></returns>
		public static IHtmlString BootstrapAlert(
			this HtmlHelper htmlHelper,
			string closeText,
			bool dismissible,
			string showOnlyWithKey = null) //TODO: Custom css classes?
		{
			return htmlHelper.BootstrapAlert(
				closeText: closeText,
				dismissible: dismissible,
				fade: false,
				showOnlyWithKey: showOnlyWithKey);
		}

		/// <summary>
		/// Renders the messages in bootstrap design.
		/// </summary>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <param name="dismissible">if set to <c>true</c> [dismissible].</param>
		/// <param name="fade">if set to <c>true</c> [fade].</param>
		/// <param name="showOnlyWithKey">Shows only alerts with specific key (only required if you want to have multiple @Html.BootstrapAlert() in one .cshtml).</param>
		/// <returns></returns>
		public static IHtmlString BootstrapAlert(
			this HtmlHelper htmlHelper,
			bool dismissible,
			bool fade,
			string showOnlyWithKey = null)
		{
			return htmlHelper.BootstrapAlert(
				closeText: "Close",
				dismissible: dismissible,
				fade: fade,
				showOnlyWithKey: showOnlyWithKey);
		}

		/// <summary>
		/// Renders the messages in bootstrap design.
		/// </summary>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <param name="closeText">The close text.</param>
		/// <param name="dismissible">if set to <c>true</c> [dismissible].</param>
		/// <param name="fade">if set to <c>true</c> [fade].</param>
		/// <param name="showOnlyWithKey">Shows only alerts with specific key (only required if you want to have multiple @Html.BootstrapAlert() in one .cshtml).</param>
		/// <returns></returns>
		public static IHtmlString BootstrapAlert(
			this HtmlHelper htmlHelper,
			string closeText,
			bool dismissible,
			bool fade,
			string showOnlyWithKey = null) //TODO: Custom css classes?
		{
			Dictionary<MessageType, string> alerts = new Dictionary<MessageType, string>();

			string key;
			string message;
			foreach (MessageType messageType in Enum.GetValues(typeof(MessageType)))
			{
				key = MakeKey(showOnlyWithKey, messageType);

				message = htmlHelper.ViewContext.ViewData.ContainsKey(key)
							? htmlHelper.ViewContext.ViewData[key].ToString()
							: htmlHelper.ViewContext.TempData.ContainsKey(key)
								? htmlHelper.ViewContext.TempData[key].ToString()
								: string.Empty;

				message = message.Trim();

				if (string.IsNullOrWhiteSpace(message)) continue;

				alerts.Add(messageType, message);
			}

			//<div class="alert alert-danger alert-dismissible fade in" role="alert">
			//	<button class="close" aria-label="Close" type="button" data-dismiss="alert"><span aria-hidden="true">×</span></button>
			//	<h4>Oh snap! You got an error!</h4>
			//	<p>Change this and that and try again. Duis mollis, est non commodo luctus, nisi erat porttitor ligula, eget lacinia odio sem nec elit. Cras mattis consectetur purus sit amet fermentum.</p>
			//	<p>
			//	<button class="btn btn-danger" type="button">Take this action</button>
			//	<button class="btn btn-default" type="button">Or do this</button>
			//	</p>
			//</div>

			if (alerts.Any())
			{
				List<TagBuilder> html = new List<TagBuilder>();

				foreach (KeyValuePair<MessageType, string> alert in alerts)
				{
					html.Add(BootstrapAlert(
						alert: alert,
						closeText: closeText,
						dismissible: dismissible,
						fade: fade));
				}

				StringBuilder sb = new StringBuilder();

				html.ForEach((alert) =>
				{
					sb.Append(alert.ToString(TagRenderMode.Normal));
				});

				return new HtmlString(sb.ToString());
			}

			return MvcHtmlString.Empty;
		}

		/// <summary>
		/// Determines whether the page has alerts.
		/// </summary>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <param name="showOnlyWithKey">Shows only alerts with specific key (only required if you want to have multiple @Html.BootstrapAlert() in one .cshtml).</param>
		/// <returns></returns>
		public static bool HasAlert(
			this HtmlHelper htmlHelper,
			string showOnlyWithKey = null)
		{
			foreach (MessageType messageType in Enum.GetValues(typeof(MessageType)))
			{
				// Check each message type!
				if (htmlHelper.HasAlert(messageType, showOnlyWithKey))
				{
					return true;
				}
			}

			return false;
		}


		/// <summary>
		/// Determines whether the page has alerts.
		/// </summary>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <param name="messageType">Type of the message.</param>
		/// <param name="showOnlyWithKey">Shows only alerts with specific key (only required if you want to have multiple @Html.BootstrapAlert() in one .cshtml).</param>
		/// <returns></returns>
		public static bool HasAlert(
			this HtmlHelper htmlHelper,
			MessageType messageType,
			string showOnlyWithKey = null)
		{
			string key = MakeKey(showOnlyWithKey, messageType);

			return htmlHelper.ViewContext.ViewData.ContainsKey(key) || htmlHelper.ViewContext.TempData.ContainsKey(key);
		}

		#region Private helpers

		private static TagBuilder BootstrapAlert(KeyValuePair<MessageType, string> alert, string closeText, bool dismissible, bool fade)
		{
			TagBuilder div = new TagBuilder("div");

			div.AddCssClass("alert");
			div.AddCssClass("alert-" + Enum.GetName(typeof(MessageType), alert.Key).ToLowerInvariant());
			div.Attributes["role"] = "alert";

			if (string.IsNullOrWhiteSpace(closeText))
			{
				closeText = "Close";
			}

			if (dismissible)
			{
				TagBuilder button = new TagBuilder("button");

				button.AddCssClass("close");
				button.Attributes["type"] = "button";
				button.Attributes["data-dismiss"] = "alert";
				button.Attributes["aria-label"] = closeText;
				button.Attributes["title"] = closeText;

				TagBuilder span = new TagBuilder("span");
				span.Attributes["aria-hidden"] = "true";
				span.InnerHtml += "&times;";

				button.InnerHtml += span;

				div.InnerHtml += button;

				if (fade) div.AddCssClass("fade in");
				div.AddCssClass("alert-dismissible");
			}

			div.InnerHtml += alert.Value;

			return div;
		}

		private static string MakeKey(string key, MessageType messageType)
		{
			string name = Enum.GetName(typeof(MessageType), messageType).ToString();

			return MakeKey(key, name);
		}

		private static string MakeKey(string key, string messageType)
		{
			if (string.IsNullOrWhiteSpace(key))
			{
				return messageType;
			}
			else
			{
				return key.Trim() + "_" + messageType;
			}
		}

		#endregion

	}
}