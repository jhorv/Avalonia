// Copyright (c) The Avalonia Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

using System;
using System.Globalization;
using Avalonia.Data;

namespace Avalonia.Markup
{
    /// <summary>
    /// Converts a binding value.
    /// </summary>
    public interface IValueConverter
    {
        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="targetType">The type of the target.</param>
        /// <param name="parameter">A user-defined parameter.</param>
        /// <param name="culture">The culture to use.</param>
        /// <returns>
        /// A <see cref="BindingNotification"/> describing the converted value or any error that
        /// occurred in converting the value.
        /// </returns>
        /// <remarks>
        /// This method should not throw exceptions. If the value is not convertible, return
        /// a <see cref="BindingNotification"/> with an exception. Any exception thrown will
        /// be treated as an application exception.
        /// </remarks>
        BindingNotification Convert(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture);

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="targetType">The type of the target.</param>
        /// <param name="parameter">A user-defined parameter.</param>
        /// <param name="culture">The culture to use.</param>
        /// <returns>The converted value.</returns>
        /// <returns>
        /// A <see cref="BindingNotification"/> describing the converted value or any error that
        /// occurred in converting the value.
        /// </returns>
        /// <remarks>
        /// This method should not throw exceptions. If the value is not convertible, return
        /// a <see cref="BindingNotification"/> with an exception. Any exception thrown will
        /// be treated as an application exception.
        /// </remarks>
        BindingNotification ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture);
    }
}
