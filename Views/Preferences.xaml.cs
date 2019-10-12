using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AmpShell.Views
{
    public class Preferences : Window
    {
        public Preferences()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
