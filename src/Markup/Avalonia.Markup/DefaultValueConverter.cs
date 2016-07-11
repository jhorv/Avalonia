// Copyright (c) The Avalonia Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Avalonia.Data;
using Avalonia.Logging;
using Avalonia.Utilities;

namespace Avalonia.Markup
{
    /// <summary>
    /// Provides a default set of value conversions for bindings that do not specify a value
    /// converter.
    /// </summary>
    public class DefaultValueConverter : IValueConverter
    {
        /// <summary>
        /// Gets an instance of a <see cref="DefaultValueConverter"/>.
        /// </summary>
        public static readonly DefaultValueConverter Instance = new DefaultValueConverter();

        /// <inheritdoc/>
        public BindingNotification Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object result;

            if (value != null && 
                (TypeUtilities.TryConvert(targetType, value, culture, out result) ||
                 TryConvertEnum(value, targetType, culture, out result)))
            {
                return new BindingNotification(result);
            }

            if (value != null || !TypeUtilities.AcceptsNull(targetType))
            {
                value = value ?? "(null)";
                var message = $"Could not convert '{value} ' to '{targetType}'";
                return new BindingNotification(new InvalidCastException(message), BindingErrorType.Error);
            }

            return BindingNotification.Null;
        }

        /// <inheritdoc/>
        public BindingNotification ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert(value, targetType, parameter, culture);
        }

        private bool TryConvertEnum(object value, Type targetType, CultureInfo cultur, out object result)
        {
            var valueTypeInfo = value.GetType().GetTypeInfo();
            var targetTypeInfo = targetType.GetTypeInfo();

            if (valueTypeInfo.IsEnum && !targetTypeInfo.IsEnum)
            {
                var enumValue = (int)value;

                if (TypeUtilities.TryCast(targetType, enumValue, out result))
                {
                    return true;
                }
            }
            else if (!valueTypeInfo.IsEnum && targetTypeInfo.IsEnum)
            {
                object intValue;

                if (TypeUtilities.TryCast(typeof(int), value, out intValue))
                {
                    result = Enum.ToObject(targetType, intValue);
                    return true;
                }
            }

            result = null;
            return false;
        }
    }
}
