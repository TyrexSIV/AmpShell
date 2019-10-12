using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AmpShell.Views
{
    public class Category : Window
    {
        public Category()
        {
            this.InitializeComponent();
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
