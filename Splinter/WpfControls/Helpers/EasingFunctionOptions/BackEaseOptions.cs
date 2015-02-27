using System.Windows;
using System.Windows.Media.Animation;

namespace Splinter.WpfControls.Helpers.EasingFunctionOptions
{
    /// <summary>
    ///     The options to be used in a <see cref="BackEase" /> easing.
    /// </summary>
    public class BackEaseOptions : EasingFunctionOptions
    {
        // Using a DependencyProperty as the backing store for Amplitude.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AmplitudeProperty =
            DependencyProperty.Register("Amplitude", typeof (double), typeof (BackEaseOptions),
                new PropertyMetadata(1.0));

        /// <summary>
        ///     Gets or sets the amplitude.
        /// </summary>
        /// <value>
        ///     The amplitude.
        /// </value>
        public double Amplitude
        {
            get { return (double) GetValue(AmplitudeProperty); }
            set { SetValue(AmplitudeProperty, value); }
        }
    }
}