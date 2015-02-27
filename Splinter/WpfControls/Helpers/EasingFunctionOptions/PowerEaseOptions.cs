using System.Windows;
using System.Windows.Media.Animation;

namespace Splinter.WpfControls.Helpers.EasingFunctionOptions
{
    /// <summary>
    ///     The options to be used in a <see cref="PowerEase" /> easing.
    /// </summary>
    public class PowerEaseOptions : EasingFunctionOptions
    {
        public static readonly DependencyProperty PowerProperty =
            DependencyProperty.Register("Power", typeof (double), typeof (PowerEaseOptions), new PropertyMetadata(2.0));

        /// <summary>
        ///     Gets or sets the exponential power of the animation interpolation.
        ///     <remarks>
        ///         For example, a value of 7 will create an animation interpolation curve that follows the formula f(t) = t7.
        ///     </remarks>
        /// </summary>
        /// <value>
        ///     The power.
        /// </value>
        public double Power
        {
            get { return (double) GetValue(PowerProperty); }
            set
            {
                if (value < 0) value = 0;
                SetValue(PowerProperty, value);
            }
        }
    }
}