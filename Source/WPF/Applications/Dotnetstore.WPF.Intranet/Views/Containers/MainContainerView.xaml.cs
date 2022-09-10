using System.Windows;
using System.Windows.Input;

namespace Dotnetstore.WPF.Intranet.Views.Containers
{
    public partial class MainContainerView
    {
        public MainContainerView()
        {
            InitializeComponent();
        }

        private void MainContainerView_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left &&
                e.ClickCount == 1)
                DragMove();
            if (e.ChangedButton == MouseButton.Left &&
                e.ClickCount == 2)
                ChangeWindowState();
        }

        private void ChangeWindowState()
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
            }
            else
            {
                if (WindowState != WindowState.Maximized) return;
                WindowState = WindowState.Normal;
            }
        }
    }
}