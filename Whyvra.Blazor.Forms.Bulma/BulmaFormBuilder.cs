using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Whyvra.Blazor.Forms.Builders;
using Whyvra.Blazor.Forms.Bulma.Components;
using Whyvra.Blazor.Forms.Components;

namespace Whyvra.Blazor.Forms.Bulma
{
    public class BulmaFormBuilder<TModel> : FormBuilder<TModel>
    {
        public override IFormBuilder<TModel> Checkbox<TProperty>(Expression<Func<TModel, TProperty>> lambda)
        {
            return Component<BulmaCheckbox<TModel>, TProperty>(lambda);
        }

        public override IFormBuilder<TModel> FileInput<TProperty>(Expression<Func<TModel, TProperty>> lambda, string accept = null)
        {
            Component<BulmaFileInput<TModel>, TProperty>(lambda);
            CurrentComponent.Parameters[nameof(BulmaFileInput<TModel>.Accept)] = accept;

            return this;
        }

        public override IFormBuilder<TModel> Input<TProperty>(Expression<Func<TModel, TProperty>> lambda)
        {
            // Add component
            Component<BulmaInput<TModel>, TProperty>(lambda);

            var name = CurrentComponent.Parameters[nameof(WhyvraComponentBase<TModel>.DisplayName)] as string;
            CurrentComponent.Parameters[nameof(BulmaComponentBase<TModel>.Placeholder)] = name;

            return this;
        }

        public override IFormBuilder<TModel> Number<TProperty>(Expression<Func<TModel, TProperty>> lambda)
        {
            // Add component
            Component<BulmaNumber<TModel>, TProperty>(lambda);

            var name = CurrentComponent.Parameters[nameof(WhyvraComponentBase<TModel>.DisplayName)] as string;
            CurrentComponent.Parameters[nameof(BulmaComponentBase<TModel>.Placeholder)] = name;

            return this;
        }

        public override IFormBuilder<TModel> TextArea<TProperty>(Expression<Func<TModel, TProperty>> lambda, int? columns = null, int? rows = null)
        {
            // Add component
            Component<BulmaTextArea<TModel>, TProperty>(lambda);

            var name = CurrentComponent.Parameters[nameof(WhyvraComponentBase<TModel>.DisplayName)] as string;
            CurrentComponent.Parameters[nameof(BulmaComponentBase<TModel>.Placeholder)] = name;
            CurrentComponent.Parameters[nameof(BulmaTextArea<TModel>.Columns)] = columns;
            CurrentComponent.Parameters[nameof(BulmaTextArea<TModel>.Rows)] = rows;

            return this;
        }

        public override IFormBuilder<TModel> TagsInput<TProperty>(Expression<Func<TModel, TProperty>> lambda, string tagCss = null)
        {
            Component<BulmaTagsInput<TModel>, TProperty>(lambda);

            var name = CurrentComponent.Parameters[nameof(WhyvraComponentBase<TModel>.DisplayName)] as string;
            CurrentComponent.Parameters[nameof(BulmaComponentBase<TModel>.Placeholder)] = name;

            return this;
        }

        public override IFormBuilder<TModel> HideOnCheck(Func<IDictionary<string, object>, bool> elementsToHide)
        {
            if (typeof(BulmaCheckbox<TModel>) == CurrentComponent.Type)
            {
                CurrentComponent.Parameters[nameof(BulmaCheckbox<TModel>.ElementsToHide)] = elementsToHide;
            }

            return this;
        }

        public override IFormBuilder<TModel> WithIcon(string icon, string iconSize = null)
        {
            if (typeof(WhyvraComponentBase<TModel>).IsAssignableFrom(CurrentComponent.Type))
            {
                CurrentComponent.Parameters[nameof(BulmaComponentBase<TModel>.Icon)] = icon;
                CurrentComponent.Parameters[nameof(BulmaComponentBase<TModel>.IconSize)] = iconSize ?? string.Empty;
            }

            return this;
        }

        public override IFormBuilder<TModel> WithPlaceholder(string placeholder)
        {
            if (typeof(WhyvraComponentBase<TModel>).IsAssignableFrom(CurrentComponent.Type))
            {
                CurrentComponent.Parameters[nameof(BulmaComponentBase<TModel>.Placeholder)] = placeholder;
            }

            return this;
        }

        public override IFormBuilder<TModel> WithText(string text)
        {
            base.WithText(text);
            if (typeof(BulmaComponentBase<TModel>).IsAssignableFrom(CurrentComponent.Type))
            {
                CurrentComponent.Parameters[nameof(BulmaComponentBase<TModel>.Placeholder)] = text;
            }

            return this;
        }
    }
}
