# OPTEN Web Mvc

Our OPTEN MVC Library

## Binders

There is a SwissDateTimeBinder to parse a swiss date to a C# DateTime.

    ModelBinders.Binders.Add(typeof(DateTime), new SwissDateTimeBinder());
    ModelBinders.Binders.Add(typeof(DateTime?), new SwissDateTimeBinder());

And a CurrencyDecimalBinder to parse strings (Html.TextBoxFor()) like 1'000 or 2'000'500.25 to a decimal.

    ModelBinders.Binders.Add(typeof(decimal), new CurrencyDecimalBinder());
    ModelBinders.Binders.Add(typeof(decimal?), new CurrencyDecimalBinder());
	

## IRenderer

RazorViewRenderer converts a .cshtml and model to a string which can be used to send it as an e-mail body or anything else :-)!
	

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