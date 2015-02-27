using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Splinter.WpfControls.Helpers;
using Splinter.WpfControls.Helpers.EasingFunctionOptions;

namespace Splinter.WpfControls
{
    /// <summary>
    ///     A <see cref="ContentControl" /> that will animate trnasition states.
    /// </summary>
    public class AnimatedContentControl : ContentControl
    {
        /// <summary>
        ///     The main content area.
        /// </summary>
        protected ContentPresenter MainContent;

        /// <summary>
        ///     The area where the old control will be drawn.
        /// </summary>
        protected Shape PaintArea;

        #region Dependency Properties

        public static readonly DependencyProperty MovementDirectionProperty =
            DependencyProperty.Register("MovementDirection", typeof (Direction), typeof (AnimatedContentControl),
                new PropertyMetadata(Direction.Left));

        public static readonly DependencyProperty EasingFunctionTypeProperty =
            DependencyProperty.Register("EasingFunctionType", typeof (EasingFunctionType),
                typeof (AnimatedContentControl), new PropertyMetadata(EasingFunctionType.CircleEase));

        public static readonly DependencyProperty EasingModeProperty =
            DependencyProperty.Register("EasingMode", typeof (EasingMode), typeof (AnimatedContentControl),
                new PropertyMetadata(EasingMode.EaseOut));

        public static readonly DependencyProperty AnimationSecondsProperty =
            DependencyProperty.Register("AnimationSeconds", typeof (double), typeof (AnimatedContentControl),
                new PropertyMetadata(1.0));


        public static readonly DependencyProperty EasingFunctionOptionsProperty =
            DependencyProperty.Register("EasingFunctionOptions", typeof (EasingFunctionOptions),
                typeof (AnimatedContentControl), new PropertyMetadata(null));

        /// <summary>
        ///     Gets or sets the movement direction for the animation.
        /// </summary>
        /// <value>
        ///     The movement direction.
        /// </value>
        public Direction MovementDirection
        {
            get { return (Direction) GetValue(MovementDirectionProperty); }
            set { SetValue(MovementDirectionProperty, value); }
        }

        /// <summary>
        ///     Gets or sets the type of the easing function.
        /// </summary>
        /// <value>
        ///     The type of the easing function.
        /// </value>
        public EasingFunctionType EasingFunctionType
        {
            get { return (EasingFunctionType) GetValue(EasingFunctionTypeProperty); }
            set { SetValue(EasingFunctionTypeProperty, value); }
        }

        /// <summary>
        ///     Gets or sets the easing mode.
        /// </summary>
        /// <value>
        ///     The easing mode.
        /// </value>
        public EasingMode EasingMode
        {
            get { return (EasingMode) GetValue(EasingModeProperty); }
            set { SetValue(EasingModeProperty, value); }
        }

        /// <summary>
        ///     Gets or sets the animation time in seconds.
        /// </summary>
        /// <value>
        ///     The animation time.
        /// </value>
        public double AnimationSeconds
        {
            get { return (double) GetValue(AnimationSecondsProperty); }
            set { SetValue(AnimationSecondsProperty, value); }
        }

        /// <summary>
        ///     Gets or sets the easing function options.
        /// </summary>
        /// <value>
        ///     The easing function options.
        /// </value>
        public EasingFunctionOptions EasingFunctionOptions
        {
            get { return (EasingFunctionOptions) GetValue(EasingFunctionOptionsProperty); }
            set { SetValue(EasingFunctionOptionsProperty, value); }
        }

        #endregion Dependency Properties

        /// <summary>
        ///     When overridden in a derived class, is invoked whenever application code or internal processes call
        ///     <see cref="M:System.Windows.FrameworkElement.ApplyTemplate" />.
        /// </summary>
        public override void OnApplyTemplate()
        {
            PaintArea = Template.FindName("PART_PaintArea", this) as Shape;
            MainContent = Template.FindName("PART_MainContent", this) as ContentPresenter;

            base.OnApplyTemplate();
        }

        /// <summary>
        ///     Called when the <see cref="P:System.Windows.Controls.ContentControl.Content" /> property changes.
        /// </summary>
        /// <param name="oldContent">The old value of the <see cref="P:System.Windows.Controls.ContentControl.Content" /> property.</param>
        /// <param name="newContent">The new value of the <see cref="P:System.Windows.Controls.ContentControl.Content" /> property.</param>
        protected override void OnContentChanged(object oldContent, object newContent)
        {
            if (PaintArea != null && MainContent != null)
            {
                PaintArea.Fill = CreateBrushFromVisual(MainContent);
                BeginAnimatedContentReplacement();
            }

            base.OnContentChanged(oldContent, newContent);
        }

        /// <summary>
        ///     Begins the animated content replacement.
        /// </summary>
        protected virtual void BeginAnimatedContentReplacement()
        {
            var newContentTransform = new TranslateTransform();
            var oldContentTransform = new TranslateTransform();

            PaintArea.RenderTransform = oldContentTransform;
            MainContent.RenderTransform = newContentTransform;
            PaintArea.Visibility = Visibility.Visible;

            var sourceAndTransform = GetSourceAndTransform();
            newContentTransform.BeginAnimation(sourceAndTransform.Item2, CreateAnimation(sourceAndTransform.Item1, 0));
            oldContentTransform.BeginAnimation(sourceAndTransform.Item2, CreateAnimation(
                0, -sourceAndTransform.Item1, (s, e) => PaintArea.Visibility = Visibility.Hidden));
        }

        /// <summary>
        ///     Creates a brush based on the current appearance of a visual element.
        ///     The brush is an ImageBrush and once created, won't update its look
        /// </summary>
        /// <param name="v">The visual element to take a snapshot of</param>
        protected virtual Brush CreateBrushFromVisual(Visual v)
        {
            if (v == null)
                throw new ArgumentNullException("v");
            var target = new RenderTargetBitmap((int) ActualWidth, (int) ActualHeight,
                96, 96, PixelFormats.Pbgra32);
            target.Render(v);
            var brush = new ImageBrush(target);
            brush.Freeze();
            return brush;
        }

        /// <summary>
        ///     Creates the animation.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <param name="whenDone">The when done.</param>
        /// <returns></returns>
        protected virtual AnimationTimeline CreateAnimation(double from, double to, EventHandler whenDone = null)
        {
            var ease = GetEasingFunction();
            ease = ApplyEasingFunctionOptions(ease);
            var duration = new Duration(TimeSpan.FromSeconds(AnimationSeconds));

            var animation = new DoubleAnimation(from, to, duration) {EasingFunction = ease};
            if (whenDone != null)
                animation.Completed += whenDone;
            animation.Freeze();
            return animation;
        }

        /// <summary>
        ///     Gets the source and transform.
        /// </summary>
        /// <returns></returns>
        protected virtual Tuple<double, DependencyProperty> GetSourceAndTransform()
        {
            double from;
            DependencyProperty translateTransformProperty;

            switch (MovementDirection)
            {
                case Direction.Left:
                    translateTransformProperty = TranslateTransform.XProperty;
                    from = ActualWidth;
                    break;
                case Direction.Right:
                    translateTransformProperty = TranslateTransform.XProperty;
                    from = -ActualWidth;
                    break;
                case Direction.Down:
                    translateTransformProperty = TranslateTransform.YProperty;
                    from = -ActualHeight;
                    break;
                default:
                    translateTransformProperty = TranslateTransform.YProperty;
                    from = ActualHeight;
                    break;
            }
            return new Tuple<double, DependencyProperty>(from, translateTransformProperty);
        }

        /// <summary>
        ///     Gets the easing function.
        /// </summary>
        /// <returns></returns>
        protected virtual IEasingFunction GetEasingFunction()
        {
            var reference = new Dictionary<Func<EasingFunctionType, bool>, IEasingFunction>
            {
                {eft => eft == EasingFunctionType.BackEase, new BackEase()},
                {eft => eft == EasingFunctionType.BounceEase, new BounceEase()},
                {eft => eft == EasingFunctionType.CircleEase, new CircleEase()},
                {eft => eft == EasingFunctionType.CubicEase, new CubicEase()},
                {eft => eft == EasingFunctionType.ElasticEase, new ElasticEase()},
                {eft => eft == EasingFunctionType.ExponentialEase, new ExponentialEase()},
                {eft => eft == EasingFunctionType.PowerEase, new PowerEase()},
                {eft => eft == EasingFunctionType.QuadraticEase, new QuadraticEase()},
                {eft => eft == EasingFunctionType.QuarticEase, new QuarticEase()},
                {eft => eft == EasingFunctionType.QuinticEase, new QuinticEase()},
                {eft => eft == EasingFunctionType.SineEase, new SineEase()}
            };
            return reference.First(param => param.Key(EasingFunctionType)).Value;
        }

        /// <summary>
        ///     Applies the easing function options.
        /// </summary>
        /// <param name="easingFunction">The easing function.</param>
        /// <returns></returns>
        protected virtual IEasingFunction ApplyEasingFunctionOptions(IEasingFunction easingFunction)
        {
            if (Equals(EasingFunctionOptions, DependencyProperty.UnsetValue) || EasingFunctionOptions == null)
                return easingFunction;

            if (easingFunction is BackEase && EasingFunctionOptions is BackEaseOptions)
                ((BackEase) easingFunction).Amplitude = ((BackEaseOptions) EasingFunctionOptions).Amplitude;

            if (easingFunction is BounceEase && EasingFunctionOptions is BounceEaseOptions)
            {
                ((BounceEase) easingFunction).Bounces = ((BounceEaseOptions) EasingFunctionOptions).Bounces;
                ((BounceEase) easingFunction).Bounciness = ((BounceEaseOptions) EasingFunctionOptions).Bounciness;
            }

            if (easingFunction is ElasticEase && EasingFunctionOptions is ElasticEaseOptions)
            {
                ((ElasticEase) easingFunction).Oscillations = ((ElasticEaseOptions) EasingFunctionOptions).Oscillations;
                ((ElasticEase) easingFunction).Springiness = ((ElasticEaseOptions) EasingFunctionOptions).Springiness;
            }

            if (easingFunction is ExponentialEase && EasingFunctionOptions is ExponentialEaseOptions)
                ((ExponentialEase) easingFunction).Exponent = ((ExponentialEaseOptions) EasingFunctionOptions).Exponent;

            if (easingFunction is PowerEase && EasingFunctionOptions is PowerEaseOptions)
                ((PowerEase) easingFunction).Power = ((PowerEaseOptions) EasingFunctionOptions).Power;


            return easingFunction;
        }
    }
}