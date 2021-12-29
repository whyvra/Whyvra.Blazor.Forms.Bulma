# Whyvra.Blazor.Forms.Bulma

[![NuGet Package](https://img.shields.io/nuget/v/Whyvra.Blazor.Forms.Bulma.svg?color=blue&style=flat-square)](https://www.nuget.org/packages/Whyvra.Blazor.Forms.Bulma/)
[![NuGet Package Download Count](https://img.shields.io/nuget/dt/Whyvra.Blazor.Forms.Bulma?color=blue&style=flat-square)](https://www.nuget.org/packages/Whyvra.Blazor.Forms.Bulma/)
[![LICENSE](https://img.shields.io/badge/license-MIT-blue?style=flat-square)](./LICENSE)
[![PRs Welcome](https://img.shields.io/badge/PRs-welcome-brightgreen.svg?style=flat-square)](http://makeapullrequest.com)

A dynamic form builder for Bulma CSS that binds to your model classes and create HTML forms for you.

## Installation

```bash
dotnet add package Whyvra.Blazor.Forms.Bulma
```

In your Blazor project, you can add a reference to this project's CSS to your `wwwroot/index.html` if you plan on using the TagsInput component:

```html
<link rel="stylesheet" type="text/css" href="_content/Whyvra.Blazor.Forms.Bulma/Whyvra.Blazor.Forms.Bulma.bundle.scp.css"/>
```

You can also find some alternative themes for Bulma [here](https://jenil.github.io/bulmaswatch/).

## Usage

Given the following model class:

```csharp
public class Contact
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}
```

The `BulmaFormBuilder<TModel>` class can be used to define elements on a form. The resulting `IFormModel<TModel>` can then be used with `WhyvraForm` to generate the corresponding HTML `<form>`.

```razor
@using System.Text.Json
@using Whyvra.Blazor.Forms

<WhyvraForm FormModel="@formModel" FormState="@FormState.New"></WhyvraForm>


<button class="button is-success" @onclick="ProcessForm">Done</button>

@code
{
  private IFormModel<Contact> formModel;

  protected override void OnInitialized()
  {
    formModel = new BulmaFormBuilder<Contact>()
      .Input(x => x.FirstName)
      .Input(x => x.LastName)
      .Input(x => x.Email)
      .Build();
  }

  private void ProcessForm()
  {
    var model = formModel.DataModel;
    Console.WriteLine(JsonSerializer.Serialize(model));
  }
}
```

## Form Validation

In order to validate the form, you can supply pass a `Func<TModel, ValidationResult>` parameter called `OnValidate` to the `FormRenderer`. The provided function should return a list of error messages for individual properties and a `bool` to indicate whether the model is valid.

Given the following validator for the `Contact` model class :

```csharp
public class ContactValidator : AbstractValidator<Contact>
{
    public ContactValidator()
    {
        RuleFor(x => x.FirstName).NotNull().NotEmpty().MaximumLength(32);
        RuleFor(x => x.LastName).NotNull().NotEmpty().MaximumLength(32);
        RuleFor(x => x.Email).EmailAddress();
    }
}
```

The `OnValidate` function returns a `ValidationResult` which indicates whether the model is valid and error messages if any. This function can be passed as the `OnValidate` parameter on the `WhyvraForm` razor component or any component inheriting from `WhyvraFormBase`.

```razor
@using System.Text.Json
@using FluentValidation
@using Whyvra.Blazor.Forms

@inject IValidator<Contact> Validator

<WhyvraForm @ref="form" FormModel="@formModel" FormState="@FormState.New" OnValidate="OnValidate"></WhyvraForm>

<button class="button is-success" @onclick="ProcessForm">Done</button>

@code
{
  private IFormModel<Contact> formModel;
  private WhyvraForm<Contact> form;

  protected override void OnInitialized()
  {
    formModel = new BulmaFormBuilder<Contact>()
      .Input(x => x.FirstName)
      .Input(x => x.LastName)
      .Input(x => x.Email)
      .Build();
  }

  private void ProcessForm()
  {
    // Trigger validation on whole form
    var result = form.ValidateForm();

    if (result.IsValid)
    {
      Console.WriteLine(JsonSerializer.Serialize(model));
    }
  }

  private ValidationResult OnValidate(Contact contact)
  {
    var result = Validator.Validate(contact);
    return new ValidationResult
    {
        IsValid = result.IsValid,
        ValidationMessages = result.Errors
            .Select(x => new { x.PropertyName, x.ErrorMessage })
            .GroupBy(x => x.PropertyName)
            .ToDictionary(x => x.Key, x => x.Select(x => x.ErrorMessage))
    };
  }
}
```

## License

Released under the [MIT License](./LICENSE).
