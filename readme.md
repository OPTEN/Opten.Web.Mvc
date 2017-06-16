# OPTEN Web Mvc

Our OPTEN MVC Library


## Attributes

To validate (jquery.validation) an E-Mail correctly you should use ```[RegexEmailAddress]``` on the property.

Varnish Attribute see: https://bitbucket.org/opten/opten-solutions/src/master/src/Opten.Web.Infrastructure/#markdown-header-varnish

## Binders

There is a SwissDateTimeBinder to parse a swiss date to a C# DateTime.

    ModelBinders.Binders.Add(typeof(DateTime), new SwissDateTimeBinder());
    ModelBinders.Binders.Add(typeof(DateTime?), new SwissDateTimeBinder());

And a CurrencyDecimalBinder to parse strings (Html.TextBoxFor()) like 1'000 or 2'000'500.25 to a decimal.

    ModelBinders.Binders.Add(typeof(decimal), new CurrencyDecimalBinder());
    ModelBinders.Binders.Add(typeof(decimal?), new CurrencyDecimalBinder());


### Validation w/ ModelState

	ModelStateMvcValidator validator = new ModelStateMvcValidator<Vehicle>(modelState: modelState);

	validator.IgnoreProperty(v => v.Id);

	validtor.IsValid();


## URL Helpers

There are URL Helpers to get current url or other things.

    @Url.Current()

TODO: Describe


## HTML Helpers

There are HTLM Helpers to make your life easier.

    @Html.If(test, "Ja", "Nein")

    @Html.If(Model != null, () => Model.Name)

    @Html.Frank("\n") => <br/>


## Bootstrap Helpers

There are a lot of Bootstrap helpers to generate HTML like ```<input class="form-control" />``` or ```<textarea rows="4" class="form-control" />``` with Bootstrap classes.

TODO: Describe also BootstrapMessageHelper and BootstrapValidationHelper