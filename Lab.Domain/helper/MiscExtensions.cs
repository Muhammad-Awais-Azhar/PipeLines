using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVX.Domain.helper
{
    public static class MiscExtensions
    {
        public static string FormatDate(this DateTime date)
        {
            return date.ToString("MM/dd/yyyy");
        }

        public static string FormatDate(this DateTime? date)
        {
            if (date.HasValue)
            {
                return date.Value.FormatDate();
            }
            else
                return string.Empty;
        }
        public static string FormatFullDate(this DateTime date)
        {
            return date.ToString("MMMM d, yyyy");
        }
        public static string FormatFullDate(this DateTime? date)
        {
            if (date.HasValue)
            {
                return date.Value.FormatFullDate();
            }
            else
                return string.Empty;
        }

        public static string ToDescription(this Enum value)
        {
            var da = (DescriptionAttribute[])(value.GetType().GetField(value.ToString())).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return da.Length > 0 ? da[0].Description : value.ToString();
        }
    }
}
