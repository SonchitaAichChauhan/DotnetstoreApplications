using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Dotnetstore.WPF.CustomControls
{
    public sealed class MenuItem : Button
    {
        public static readonly DependencyProperty MenuItemFontSizeProperty = DependencyProperty.Register(
            "MenuItemFontSize", typeof(double), typeof(MenuItem), new PropertyMetadata(null));

        public static readonly DependencyProperty MenuItemForegroundProperty = DependencyProperty.Register(
            "MenuItemForeground", typeof(SolidColorBrush), typeof(MenuItem), new PropertyMetadata(null));

        public static readonly DependencyProperty MenuItemIconHeightProperty = DependencyProperty.Register(
            "MenuItemIconHeight", typeof(double), typeof(MenuItem), new PropertyMetadata(null));

        public static readonly DependencyProperty MenuItemIconProperty = DependencyProperty.Register(
            "MenuItemIcon", typeof(PackIconKind), typeof(MenuItem), new PropertyMetadata(null));

        public static readonly DependencyProperty MenuItemIconWidthProperty = DependencyProperty.Register(
            "MenuItemIconWidth", typeof(double), typeof(MenuItem), new PropertyMetadata(null));

        public static readonly DependencyProperty MenuItemOnMouseOverColorProperty = DependencyProperty.Register(
            "MenuItemOnMouseOverColor", typeof(Color), typeof(MenuItem), new PropertyMetadata(null));

        public static readonly DependencyProperty MenuItemTextProperty = DependencyProperty.Register(
            "MenuItemText", typeof(string), typeof(MenuItem), new PropertyMetadata(null));

        static MenuItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MenuItem), new FrameworkPropertyMetadata(typeof(MenuItem)));
        }

        public double MenuItemFontSize
        {
            get => (double)GetValue(MenuItemFontSizeProperty);
            set => SetValue(MenuItemFontSizeProperty, value);
        }

        public SolidColorBrush MenuItemForeground
        {
            get => (SolidColorBrush)GetValue(MenuItemForegroundProperty);
            set => SetValue(MenuItemForegroundProperty, value);
        }

        public PackIconKind MenuItemIcon
        {
            get => (PackIconKind)GetValue(MenuItemIconProperty);
            set => SetValue(MenuItemIconProperty, value);
        }

        public double MenuItemIconHeight
        {
            get => (double)GetValue(MenuItemIconHeightProperty);
            set => SetValue(MenuItemIconHeightProperty, value);
        }

        public double MenuItemIconWidth
        {
            get => (double)GetValue(MenuItemIconWidthProperty);
            set => SetValue(MenuItemIconWidthProperty, value);
        }

        public Color MenuItemOnMouseOverColor
        {
            get => (Color)GetValue(MenuItemOnMouseOverColorProperty);
            set => SetValue(MenuItemOnMouseOverColorProperty, value);
        }

        public string MenuItemText
        {
            get => (string)GetValue(MenuItemTextProperty);
            set => SetValue(MenuItemTextProperty, value);
        }
    }
}
