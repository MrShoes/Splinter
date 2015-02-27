using System.Windows.Controls;

namespace Splinter.WpfControls
{
    /// <summary>
    ///     A CheckBox that allows the nullable bool state.
    /// </summary>
    public class NullableCheckBox : CheckBox
    {
        /// <summary>
        ///     Called by the <see cref="M:System.Windows.Controls.Primitives.ToggleButton.OnClick" /> method to implement toggle
        ///     behavior.
        /// </summary>
        protected override void OnToggle()
        {
            if (!IsChecked.HasValue)
                IsChecked = true;
            base.OnToggle();
        }
    }
}