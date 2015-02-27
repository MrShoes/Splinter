using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Splinter.WpfControls.Converters
{
    /// <summary>
    ///     Performs an inverse operation to the <see cref="BooleanToVisibilityConverter" /> class.
    /// </summary>
    public class InverseBooleanToVisibilityConverter : IValueConverter
    {
        /// <summary>
        ///     Modifies the source data before passing it to the target for display in the UI.
        /// </summary>
        /// <param name="value">The source data being passed to the target.</param>
        /// <param name="targetType">
        ///     The type of the target property, as a type reference (System.Type for Microsoft .NET, a
        ///     TypeName helper struct for Visual C++ component extensions (C++/CX)).
        /// </param>
        /// <param name="parameter">An optional parameter to be used in the converter logic.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        ///     The value to be passed to the target dependency property.
        /// </returns>
        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var flag = false;
            if (value is bool)
            {
                flag = (bool) value;
            }
            return (flag ? Visibility.Collapsed : Visibility.Visible);
        }

        /// <summary>
        ///     Modifies the target data before passing it to the source object. This method is called only in TwoWay bindings.
        /// </summary>
        /// <param name="value">The target data being passed to the source.</param>
        /// <param name="targetType">
        ///     The type of the target property, as a type reference (System.Type for Microsoft .NET, a
        ///     TypeName helper struct for Visual C++ component extensions (C++/CX)).
        /// </param>
        /// <param name="parameter">An optional parameter to be used in the converter logic.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        ///     The value to be passed to the source object.
        /// </returns>
        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((value is Visibility) && (((Visibility) value) == Visibility.Collapsed));
        }
    }
}