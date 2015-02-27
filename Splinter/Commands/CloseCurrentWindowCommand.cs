using System.Windows;

namespace Splinter.Commands
{
    /// <summary>
    ///     Command used to close the current window.
    ///     <remarks>
    ///         To bind this correctly in XAML, use the following syntax:
    ///         CommandParameter = "{Binding RelativeSource={RelativeSource Self}}"
    ///     </remarks>
    /// </summary>
    internal class CloseCurrentWindowCommand : Command
    {
        /// <summary>
        ///     Executes the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public override void Execute(object parameter)
        {
            var dependencyObject = parameter as DependencyObject;
            if (dependencyObject == null) return;

            var currentWindow = Window.GetWindow(dependencyObject);
            if (currentWindow == null) return;

            currentWindow.Close();
        }
    }
}