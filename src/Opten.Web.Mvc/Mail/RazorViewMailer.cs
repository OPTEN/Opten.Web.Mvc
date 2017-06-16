using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Opten.Web.Infrastructure.Mail // extend Infrastructure
{
	/// <summary>
	/// A SMTP mailer to send Razor Views as HTML (e.g. from web.config settings).
	/// </summary>
	public class RazorViewMailer
	{

		#region Public properties

		/// <summary>
		/// Gets or sets a value indicating whether [trim body].
		/// </summary>
		/// <value>
		///   <c>true</c> if [trim body]; otherwise, <c>false</c>.
		/// </value>
		public bool TrimBody { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is body HTML.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance is body HTML; otherwise, <c>false</c>.
		/// </value>
		public bool IsBodyHtml { get; set; }

		#endregion

		#region Private fields

		private readonly Lazy<SimpleMailer> _mailer;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="RazorViewMailer" /> class.
		/// </summary>
		/// <param name="from">From.</param>
		/// <param name="displayName">The display name.</param>
		/// <param name="to">To.</param>
		/// <param name="cc">The cc.</param>
		/// <param name="bcc">The BCC.</param>
		/// <param name="replyTo">The reply to.</param>
		public RazorViewMailer(
			string from,
			string displayName,
			string[] to = null,
			string[] cc = null,
			string[] bcc = null,
			string[] replyTo = null)
		{
			this.TrimBody = true; // trim by default
			this.IsBodyHtml = true; // HTML by default

			_mailer = new Lazy<SimpleMailer>(() =>
			{
				SimpleMailer mailer = new SimpleMailer(
					from: from,
					displayName: displayName,
					to: to,
					cc: cc,
					bcc: bcc,
					replyTo: replyTo);

				mailer.IsBodyHtml = this.IsBodyHtml;

				return mailer;
			});
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RazorViewMailer" /> class.
		/// </summary>
		/// <param name="from">From.</param>
		/// <param name="displayName">The display name.</param>
		/// <param name="to">To.</param>
		/// <param name="cc">The cc.</param>
		/// <param name="bcc">The BCC.</param>
		/// <param name="replyTo">The reply to.</param>
		public RazorViewMailer(
			string from,
			string displayName,
			string to,
			string[] cc = null,
			string[] bcc = null,
			string[] replyTo = null)
			: this(from, displayName, new string[] { to }, cc, bcc, replyTo)
		{ }

		#endregion

		/// <summary>
		/// Sends the e-mail from web configuration settings.
		/// </summary>
		/// <param name="subject">The subject.</param>
		/// <param name="viewName">Name of the view.</param>
		/// <param name="model">The model.</param>
		public void SendFromWebConfigSettings(
			string subject,
			string viewName,
			object model)
		{
			_mailer.Value.SendFromWebConfigSettings(
				subject: subject,
				message: RenderViewAsString(
					viewName: viewName,
					model: model
				));
		}

		/// <summary>
		/// Sends the e-mail from web configuration settings.
		/// </summary>
		/// <param name="subject">The subject.</param>
		/// <param name="viewName">Name of the view.</param>
		/// <param name="model">The model.</param>
		/// <param name="attachments">The attachments.</param>
		public void SendFromWebConfigSettings(
			string subject,
			string viewName,
			object model,
			IEnumerable<Attachment> attachments)
		{
			_mailer.Value.SendFromWebConfigSettings(
				subject: subject,
				message: RenderViewAsString(
					viewName: viewName,
					model: model
				),
				attachments: attachments);
		}

		#region Private methods

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

		private string RenderViewAsString(string viewName, object model)
		{
			using (Controller controller = CreateController<MailTemplateController>())
			{
				using (StringWriter writer = new StringWriter())
				{
					ViewDataDictionary viewData = new ViewDataDictionary(model);
					TempDataDictionary tempData = new TempDataDictionary();

					ViewEngineResult viewResult = ViewEngines.Engines.FindView(controller.ControllerContext, viewName, null);
					ViewContext viewContext = new ViewContext(controller.ControllerContext, viewResult.View, viewData, tempData, writer);
					viewResult.View.Render(viewContext, writer);

					string output = writer.GetStringBuilder().ToString();

					if (this.TrimBody)
					{
						output = output.Trim();
					}

					return output;
				}
			}
		}

		//TODO: Will this be a problem in the future?
		// -> If you use this you cannot create a second Controller with this name
		private class MailTemplateController : Controller { }

		#endregion

	}
}