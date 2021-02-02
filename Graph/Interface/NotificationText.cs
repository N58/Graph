using Graph.Graphics;
using Graph.Windows;
using System;
using System.Windows;
using System.Windows.Threading;

namespace Graph.Interface
{
    static class NotificationText
    {
        private static MainWindow window = (MainWindow)Application.Current.MainWindow;

        public static void SetError(string text)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                window.Notification.Foreground = VisualConfig.TextRedColor;
                window.Notification.Text = text;
            }), DispatcherPriority.Background);
        }

        public static void SetConfirmation(string text)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                window.Notification.Foreground = VisualConfig.TextGreenColor;
                window.Notification.Text = text;
            }), DispatcherPriority.Background);
        }

        public static void SetInfo(string text)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                window.Notification.Foreground = VisualConfig.TextBlueColor;
                window.Notification.Text = text;
            }), DispatcherPriority.Background);
        }
    }
}
