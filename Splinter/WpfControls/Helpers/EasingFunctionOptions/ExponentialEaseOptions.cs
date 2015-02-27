using System.Windows;
using System.Windows.Media.Animation;

namespace Splinter.WpfControls.Helpers.EasingFunctionOptions
{
    /// <summary>
    ///     The options to be used in a <see cref="ExponentialEase" /> easing.
    /// </summary>
    public class ExponentialEaseOptions : EasingFunctionOptions
    {
        public static readonly DependencyProperty ExponentProperty =
            DependencyProperty.Register("Exponent", typeof (double), typeof (ExponentialEaseOptions),
                new PropertyMetadata(2.0));

        /// <summary>
        ///     Gets or sets the exponent used to determine the interpolation.
        /// </summary>
        /// <value>
        ///     The exponent.
        /// </value>
        public double Exponent
        {
            get { return (double) GetValue(ExponentProperty); }
            set { SetValue(ExponentProperty, value); }
        }
    }
}