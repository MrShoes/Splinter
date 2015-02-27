using System.Windows;
using System.Windows.Media.Animation;

namespace Splinter.WpfControls.Helpers.EasingFunctionOptions
{
    /// <summary>
    ///     The options to be used by a <see cref="IEasingFunction" />. Classes that inherit from
    ///     this type must expose Dependency Properties to allow XAML
    ///     control.
    /// </summary>
    public abstract class EasingFunctionOptions : DependencyObject
    {
    }
}