using ShoppingApp.Core.Services;
using System.Windows.Controls;
using ShoppingApp.Core;
using System.Windows;
using System;

namespace ShoppingApp
{
    public partial class MainPage : Page, IMessageService
    {
        private MainWindow _mainWindowInstance;
        public MainPage()
        {
            InitializeComponent();

            DataContext = new MainPageViewModel(this);

            _mainWindowInstance = (MainWindow)Application.Current.MainWindow;
        }

        public void CloseApplication()
        {
            App.Current.Shutdown();
        }

        public void MinimalizeApplication()
        {
            _mainWindowInstance.MinimalizeApplicationMainWindow();
        }
    }
}
