using Avalonia;
using Avalonia.Markup.Xaml;

namespace AmpShell
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
