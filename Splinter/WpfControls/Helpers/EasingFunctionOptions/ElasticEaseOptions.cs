using System.Windows;
using System.Windows.Media.Animation;

namespace Splinter.WpfControls.Helpers.EasingFunctionOptions
{
    /// <summary>
    ///     The options to be used in a <see cref="ElasticEase" /> easing.
    /// </summary>
    public class ElasticEaseOptions : EasingFunctionOptions
    {
        // Using a DependencyProperty as the backing store for Oscillations.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OscillationsProperty =
            DependencyProperty.Register("Oscillations", typeof (int), typeof (ElasticEaseOptions),
                new PropertyMetadata(3));


        // Using a DependencyProperty as the backing store for Springiness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SpringinessProperty =
            DependencyProperty.Register("Springiness", typeof (double), typeof (ElasticEaseOptions),
                new PropertyMetadata(3.0));

        /// <summary>
        ///     Gets or sets the number of times the target slides back and forth over the animation destination.
        /// </summary>
        /// <value>
        ///     The oscillations.
        /// </value>
        public int Oscillations
        {
            get { return (int) GetValue(OscillationsProperty); }
            set
            {
                if (value < 0) value = 0;
                SetValue(OscillationsProperty, value);
            }
        }

        /// <summary>
        ///     Gets or sets the stiffness of the spring.
        ///     <remarks>
        ///         The smaller the Springiness value is, the stiffer the spring and the faster the elasticity decreases in
        ///         intensity over each oscillation.
        ///     </remarks>
        /// </summary>
        /// <value>
        ///     The springiness.
        /// </value>
        public double Springiness
        {
            get { return (double) GetValue(SpringinessProperty); }
            set { SetValue(SpringinessProperty, value); }
        }
    }
}