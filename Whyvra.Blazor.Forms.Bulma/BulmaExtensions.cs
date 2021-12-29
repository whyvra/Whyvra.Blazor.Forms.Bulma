using Whyvra.Blazor.Forms.Bulma.Components;

namespace Whyvra.Blazor.Forms.Bulma
{
    public static class BulmaExtensions
    {
        public static IFormBuilder<TModel> WithButtonText<TModel>(this IFormBuilder<TModel> formBuilder, string text)
        {
            var current = formBuilder.CurrentComponent;
            if (typeof(BulmaFileInput<TModel>).IsAssignableFrom(current.Type))
            {
                current.Parameters[nameof(BulmaFileInput<TModel>.ButtonText)] = text;
            }

            return formBuilder;
        }

        public static IFormBuilder<TModel> WithEmptyValue<TModel>(this IFormBuilder<TModel> formBuilder, string empty)
        {
            var current = formBuilder.CurrentComponent;
            if (typeof(BulmaTagsInput<TModel>).IsAssignableFrom(current.Type))
            {
                current.Parameters[nameof(BulmaTagsInput<TModel>.EmptyValue)] = empty;
            }

            return formBuilder;
        }
    }
}
