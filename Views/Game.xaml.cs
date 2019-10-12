using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AmpShell.Views
{
    public class Game : Window
    {
        public Game()
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
