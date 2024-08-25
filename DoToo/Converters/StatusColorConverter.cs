using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace DoToo.Converters
{
    public  class StatusColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture )
        {
            return (Color)Application.Current.Resources[(bool)value ? "CompletedColor" : "ActiveColor"];
        }

        public object ConvertBack(object value, Type target, object parameter, CultureInfo culture)
        {
            return null;
        }


    }
}
