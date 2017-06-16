using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace Opten.Web.Mvc.Attributes
{

#pragma warning disable 1591

	//TODO: Why not: https://bitbucket.org/opten/opten.umbraco.common/src/7f8667912bf88955c5ab88218f2c492358800cdc/src/Opten.Umbraco.Common/Attributes/DictionaryEmailAddressAttribute.cs?at=master&fileviewer=file-view-default
	// or: https://github.com/Microsoft/referencesource/blob/master/System.ComponentModel.DataAnnotations/DataAnnotations/EmailAddressAttribute.cs
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
	public class RegexEmailAddressAttribute : DataTypeAttribute, IClientValidatable
	{

		// https://github.com/Microsoft/referencesource/blob/master/System.ComponentModel.DataAnnotations/DataAnnotations/EmailAddressAttribute.cs
		// This attribute provides server-side email validation equivalent to jquery validate,
		// and therefore shares the same regular expression.
		private static Regex _regex = new Regex(@"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
		private readonly string _errorMessage;

		public RegexEmailAddressAttribute(string errorMessage = "Das Feld E-Mail-Adresse enthält keine gültige E-Mail-Adresse.")
			: base(DataType.EmailAddress)
		{
			_errorMessage = errorMessage;
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (value == null)
			{
				return ValidationResult.Success;
			}

			string valueAsString = value as string;
			if (valueAsString != null && _regex.Match(valueAsString).Length > 0)
			{
				return ValidationResult.Success;
			}
			else
			{
				return new ValidationResult(
					_errorMessage,
					new[] { validationContext.MemberName });
			}
		}

		public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
		{
			ModelClientValidationRule rule = new ModelClientValidationRule
			{
				ErrorMessage = _errorMessage,
				ValidationType = "email",
			};

			rule.ValidationParameters.Add("pattern", _regex);

			yield return rule;
		}

	}

#pragma warning restore 1591

}