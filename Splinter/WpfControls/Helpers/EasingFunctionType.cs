using System.Windows.Media.Animation;

namespace Splinter.WpfControls.Helpers
{
    /// <summary>
    ///     Describes the <see cref="IEasingFunction" /> to be used when animating.
    /// </summary>
    public enum EasingFunctionType
    {
        /// <summary>
        ///     Retracts the motion of an animation slightly before it begins to animate in the path indicated.
        /// </summary>
        BackEase,

        /// <summary>
        ///     Creates a bouncing effect.
        /// </summary>
        BounceEase,

        /// <summary>
        ///     Creates an animation that accelerates and/or decelerates using a circular function.
        /// </summary>
        CircleEase,

        /// <summary>
        ///     Creates an animation that accelerates and/or decelerates using the formula f(t) = t3.
        /// </summary>
        CubicEase,

        /// <summary>
        ///     Creates an animation that resembles a spring oscillating back and forth until it comes to rest.
        /// </summary>
        ElasticEase,

        /// <summary>
        ///     Creates an animation that accelerates and/or decelerates using an exponential formula.
        /// </summary>
        ExponentialEase,

        /// <summary>
        ///     Creates an animation that accelerates and/or decelerates using the formula f(t) = tp where p is equal to the Power
        ///     property.
        /// </summary>
        PowerEase,

        /// <summary>
        ///     Creates an animation that accelerates and/or decelerates using the formula f(t) = t2.
        /// </summary>
        QuadraticEase,

        /// <summary>
        ///     Creates an animation that accelerates and/or decelerates using the formula f(t) = t4.
        /// </summary>
        QuarticEase,

        /// <summary>
        ///     Create an animation that accelerates and/or decelerates using the formula f(t) = t5.
        /// </summary>
        QuinticEase,

        /// <summary>
        ///     Creates an animation that accelerates and/or decelerates using a sine formula.
        /// </summary>
        SineEase
    }
}