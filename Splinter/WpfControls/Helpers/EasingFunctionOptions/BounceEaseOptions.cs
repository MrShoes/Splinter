using System.Windows;
using System.Windows.Media.Animation;

namespace Splinter.WpfControls.Helpers.EasingFunctionOptions
{
    /// <summary>
    ///     The options to be used in a <see cref="BounceEase" /> easing.
    /// </summary>
    public class BounceEaseOptions : EasingFunctionOptions
    {
        public static readonly DependencyProperty BouncesProperty =
            DependencyProperty.Register("Bounces", typeof (int), typeof (BounceEaseOptions), new PropertyMetadata(3));

        public static readonly DependencyProperty BouncinessProperty =
            DependencyProperty.Register("Bounciness", typeof (double), typeof (BounceEaseOptions),
                new PropertyMetadata(2.0));

        /// <summary>
        ///     Gets or sets the number of bounces.
        /// </summary>
        /// <value>
        ///     The bounces.
        /// </value>
        public int Bounces
        {
            get { return (int) GetValue(BouncesProperty); }
            set
            {
                if (value < 0) value = 0;
                SetValue(BouncesProperty, value);
            }
        }

        /// <summary>
        ///     Gets or sets the bounciness.
        ///     <remarks>
        ///         Low values of this property result in bounces with little loss of height between bounces (more bouncy)
        ///         while high values result in dampened bounces (less bouncy).
        ///     </remarks>
        /// </summary>
        /// <value>
        ///     The bounciness.
        /// </value>
        public double Bounciness
        {
            get { return (double) GetValue(BouncinessProperty); }
            set { SetValue(BouncinessProperty, value); }
        }
    }
}