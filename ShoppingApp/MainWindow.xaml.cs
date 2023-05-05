using System.Windows;
using System.Windows.Input;
using ShoppingApp.Core;
using ShoppingApp.Core.Services;

namespace ShoppingApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        public void MinimalizeApplicationMainWindow()

        {
            WindowState = WindowState.Minimized;
        }
    }
}
