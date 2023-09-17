using System;
using System.Windows.Threading;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace Tydzien7Lekcja27ZD
{
    public partial class App
    {
        private void App_OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            var metroWindow = Current.MainWindow as MetroWindow;
            metroWindow.ShowMessageAsync(
                "Nieoczekiwany wyjątek",
                "Wystąpił nieoczekiwany wyjątek:" +
                Environment.NewLine +
                e.Exception.Message
                );

            e.Handled = true;
        }
    }
}
