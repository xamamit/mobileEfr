using System;
using System.Globalization;
using Xamarin.Forms;
using static System.Math;

namespace Acs.Mobile.ESig.Converters
{
    public class DoubleToIntConverter : IValueConverter
    {
        /// <summary>
        /// Converts a <c>double</c> to an <c>integer</c> and rounds the <c>integer</c> using 
        /// rounding rules.
        /// </summary>
        /// <param name="value">The <c>double</c> to be converted.</param>
        /// <returns>An <c>integer</c> containing the rounded value of <paramref name="value"/> or 
        /// <c>null</c> if <paramref name="value"/> is neither a <c>decimal</c> nor <c>null</c>.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (null == value) { return null; }


            // would use 'as' instead of 'is' for a small performance gain, but it can only be used with
            // nullable or reference types, and we can't guarantee that's what's passed in.
            if (value is decimal)
            {
                return Decimal.ToInt32(Round((Decimal)value));
            }

            return null;
        }

        /// <summary>Throws a <see cref="NotImplementedException"/> under all conditions.</summary>
        /// <returns>Throws a <see cref="NotImplementedException"/> under all conditions.</returns>
        /// <exception cref="NotImplementedException">Throws exception under all conditions.</exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}