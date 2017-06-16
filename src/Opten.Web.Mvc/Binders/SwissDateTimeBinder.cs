using Opten.Common.Parsers;
using System;
using System.Web.Mvc;

namespace Opten.Web.Mvc.Binders
{
	/// <summary>
	/// Converts an input value to a swiss date.
	/// </summary>
	/// <seealso cref="System.Web.Mvc.IModelBinder" />
	public class SwissDateTimeBinder : IModelBinder
	{

		//TODO: Automatically binded? Or AppSettings?

		/// <summary>
		/// Binds the model to a value by using the specified controller context and binding context.
		/// </summary>
		/// <param name="controllerContext">The controller context.</param>
		/// <param name="bindingContext">The binding context.</param>
		/// <returns>
		/// The bound value.
		/// </returns>
		public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			if (controllerContext == null) throw new ArgumentNullException("controllerContext");
			if (bindingContext == null) throw new ArgumentNullException("bindingContext");

			ValueProviderResult value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

			//if (value == null)
			//	throw new ArgumentNullException("Value is null", bindingContext.ModelName);

			if (value == null || string.IsNullOrWhiteSpace(value.AttemptedValue)) return null;

			bindingContext.ModelState.SetModelValue(bindingContext.ModelName, value);

			try
			{
				//TODO: Check for attribute? E.g. [SwissDate]
				if (DateTimeParser.IsSwissDate(value.AttemptedValue))
				{
					return DateTimeParser.ParseSwissDateTimeString(value.AttemptedValue);
				}

				return DateTime.Parse(value.AttemptedValue);
			}
			catch (Exception ex)
			{
				bindingContext.ModelState.AddModelError(bindingContext.ModelName, ex);

				return null;
			}
		}

	}
}