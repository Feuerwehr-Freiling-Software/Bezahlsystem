using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using MudBlazor.Utilities;

namespace Paymentsystem.Client.Shared
{
    public class CustomThemeProvider : BaseMudThemeProvider
    {
        protected override void BuildRenderTree(RenderTreeBuilder __builder)
        {
            Theme = GenerateApecTheme();
            if (!base.DefaultScrollbar)
            {
                __builder.AddContent(0, (MarkupString)BuildMudBlazorScrollbar());
            }

            __builder.AddMarkupContent(1, "<style>\n    .mud-chart-serie:hover {\n        filter: url(#lighten);\n    }\n</style>\n\n");
            __builder.AddContent(2, (MarkupString)BuildTheme());
            __builder.AddMarkupContent(3, "\n\n\n");
            __builder.OpenComponent<MudPopoverProvider>(4);
            __builder.CloseComponent();
        }

        private MudTheme GenerateApecTheme()
        {
            return new MudTheme()
            {
                Palette = new Palette()
                {
                    AppbarBackground = new MudColor("#647687"),
                    Primary = new MudColor("#647687"),
                    TextPrimary = new MudColor("#000000FF"),
                    Secondary = new MudColor("#3A4147"),
                    TextSecondary = new MudColor("#000000")
                },
                PaletteDark = new Palette()
                {
                    AppbarBackground = new MudColor("#647687"),
                    Primary = new MudColor("#647687"),
                    TextPrimary = new MudColor("#FFFFFFFF"),
                    Secondary = new MudColor("#3A4147"),
                    TextSecondary = new MudColor("#FFFFFFFF"),
                    Background = new MudColor("#2e2e2e"),
                    Surface = new MudColor("#252525"),
                    LinesDefault = new MudColor("#576573"),
                    TableLines = new MudColor("#576573"),
                    ActionDisabledBackground = new MudColor("#878787"),
                    TextDisabled = new MudColor("#ababab"),
                    LinesInputs = new MudColor("#576573"),
                    ActionDefault = new MudColor("#647687")
                                        
                }
            };
        }
    }
}
