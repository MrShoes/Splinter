using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Splinter
{
    /// <summary>
    ///     A base class for all View Models used in the framework.
    /// </summary>
    public abstract class ViewModel : INotifyPropertyChanged
    {
        /// <summary>
        ///     Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Sets the property and raises notifications of change.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="field">The field.</param>
        /// <param name="value">The value.</param>
        /// <param name="property">The property.</param>
        protected virtual void SetPropertyAndNotify<T>(ref T field, T value, Expression<Func<object>> extraProperty,
            [CallerMemberName]string  propertyName = null)
        {
            if (Equals(field, value)) return;
            field = value;
            OnPropertyChanged(propertyName);
            OnPropertyChanged(extraProperty);
        }

        /// <summary>
        ///     Sets the property and raises notifications of change.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="field">The field.</param>
        /// <param name="value">The value.</param>
        /// <param name="property">The property.</param>
        /// <param name="extraProperties">The extra properties to notify.</param>
        protected virtual void SetPropertyAndNotify<T>(ref T field, T value,
            IEnumerable<Expression<Func<object>>> extraProperties,
                [CallerMemberName]string  propertyName = null)
        {
            if (Equals(field, value)) return;
            field = value;
            OnPropertyChanged(propertyName);
            foreach (var extraProperty in extraProperties)
            {
                OnPropertyChanged(extraProperty);
            }
        }

        /// <summary>
        ///     Sets the property and raises notifications of change.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="field">The field.</param>
        /// <param name="value">The value.</param>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void SetPropertyAndNotify<T>(ref T field, T value,
            [CallerMemberName] string propertyName = null)
        {
            if (Equals(field, value)) return;
            field = value;
            OnPropertyChanged(propertyName);
        }

        /// <summary>
        ///     Sets the property and raises notifications of change.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="field">The field.</param>
        /// <param name="value">The value.</param>
        /// <param name="extraPropertyNames">The extra property names to be notified on change.</param>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void SetPropertyAndNotify<T>(ref T field, T value, IEnumerable<string> extraPropertyNames,
            [CallerMemberName] string propertyName = null)
        {
            if (Equals(field, value)) return;
            field = value;
            OnPropertyChanged(propertyName);
            foreach (var extraPropertyName in extraPropertyNames)
            {
                OnPropertyChanged(extraPropertyName);
            }
        }

        /// <summary>
        ///     Called when  a property has changed.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        ///     Called when  a property has changed.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="changedProperty">The changed property.</param>
        protected virtual void OnPropertyChanged<TProperty>(Expression<Func<TProperty>> changedProperty)
        {
            var name = ((MemberExpression) changedProperty.Body).Member.Name;
            OnPropertyChanged(name);
        }
    }
}