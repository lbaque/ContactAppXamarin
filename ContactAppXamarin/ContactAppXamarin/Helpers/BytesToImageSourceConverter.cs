using System;
using System.Globalization;
using System.IO;
using Xamarin.Forms;

namespace ContactAppXamarin.Helpers
{
    public class BytesToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is byte[] bytes)
            {
                ImageSource imageSource = ImageSource.FromStream(() => new MemoryStream(bytes));
                return imageSource;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
