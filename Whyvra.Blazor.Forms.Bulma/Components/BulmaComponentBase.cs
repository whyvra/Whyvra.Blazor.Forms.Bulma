using Microsoft.AspNetCore.Components;

namespace Whyvra.Blazor.Forms.Components
{
    public abstract class BulmaComponentBase<TModel> : WhyvraComponentBase<TModel>
    {
        [Parameter]
        public string Icon { get; set; }

        [Parameter]
        public string IconSize { get; set; }

        [Parameter]
        public string Placeholder { get; set; }
    }
}
