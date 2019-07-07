using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Open4Tech.Helper
{
    public class WindowManager
    {
        public static Window CreateElementWindow(object viewModel, string title, string controlPath)
        {
            var window = new Window();
            window.Title = title;
            window.Background = Brushes.White;
            window.Foreground = Brushes.Black;
            window.Height = 450;
            window.Width = 800;
            window.WindowState = WindowState.Normal;
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            var controlAssembly = Assembly.Load("Open4Tech");
            var controlType = controlAssembly.GetType(controlPath);
            var newControl = Activator.CreateInstance(controlType) as UserControl;
            newControl.DataContext = viewModel;
            window.Content = newControl;

            return window;
        }
    }
}
