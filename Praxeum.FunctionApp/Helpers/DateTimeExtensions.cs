using System;
using System.Collections.Generic;
using System.Text;

namespace Praxeum.FunctionApp.Helpers
{
    public static class DateTimeExtensions
    {
        public static DateTime SubtractDays(
            this DateTime obj,
            double value)
        {
            return obj.AddDays(-value);
        }

        public static DateTime SubtractHours(
            this DateTime obj,
            double value)
        {
            return obj.AddHours(-value);
        }

        public static DateTime SubtractMinutes(
            this DateTime obj,
            double value)
        {
            return obj.AddMinutes(-value);
        }
    }
}
