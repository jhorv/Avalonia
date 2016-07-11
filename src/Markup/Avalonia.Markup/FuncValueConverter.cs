// Copyright (c) The Avalonia Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

using System;
using System.Globalization;
using Avalonia.Data;
using Avalonia.Utilities;

namespace Avalonia.Markup
{
    /// <summary>
    /// A general purpose <see cref="IValueConverter"/> that uses a <see cref="Func{T1, TResult}"/>
    /// to provide the converter logic.
    /// </summary>
    /// <typeparam name="TIn">The input type.</typeparam>
    /// <typeparam name="TOut">The output type.</typeparam>
    public class FuncValueConverter<TIn, TOut> : IValueConverter
    {
        private readonly Func<TIn, TOut> _convert;

        /// <summary>
        /// Initializes a new instance of the <see cref="FuncValueConverter{TIn, TOut}"/> class.
        /// </summary>
        /// <param name="convert">The convert function.</param>
        public FuncValueConverter(Func<TIn, TOut> convert)
        {
            _convert = convert;
        }

        /// <inheritdoc/>
        public BindingNotification Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TIn || (value == null && TypeUtilities.AcceptsNull(typeof(TIn))))
            {
                try
                {
                    return new BindingNotification(_convert((TIn)value));
                }
                catch (Exception e)
                {
                    return new BindingNotification(e, BindingErrorType.Error);
                }
            }
            else
            {
                return BindingNotification.UnsetValue;
            }
        }

        /// <inheritdoc/>
        public BindingNotification ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
