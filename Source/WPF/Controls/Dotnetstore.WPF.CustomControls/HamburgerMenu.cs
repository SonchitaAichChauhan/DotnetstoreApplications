using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Dotnetstore.WPF.CustomControls
{
    public sealed class HamburgerMenu : Control
    {
        static HamburgerMenu()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HamburgerMenu), new FrameworkPropertyMetadata(typeof(HamburgerMenu)));
        }

        public static readonly DependencyProperty ContentProperty = DependencyProperty.Register(
            "Content", typeof(object), typeof(HamburgerMenu), new PropertyMetadata(null));

        public static readonly DependencyProperty HamburgerMenuBackgroundProperty = DependencyProperty.Register(
            "HamburgerMenuBackground", typeof(Color), typeof(MenuItem), new PropertyMetadata(null));

        public Color HamburgerMenuBackground
        {
            get => (Color)GetValue(HamburgerMenuBackgroundProperty);
            set => SetValue(HamburgerMenuBackgroundProperty, value);
        }

        public object Content
        {
            get => GetValue(ContentProperty);
            set => SetValue(ContentProperty, value);
        }
    }
}
