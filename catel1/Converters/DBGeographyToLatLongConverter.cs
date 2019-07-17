using System.ComponentModel;
using System.Data.Entity.Spatial;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace InfConstractions.Converters
{
    [TypeConverter(typeof(DbGeography))]
    public class DBGeographyToLatLongConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }
            else
                if ((value is DbGeography) & ((DbGeography)value).SpatialTypeName == "Point")
            {
                string s = ((DbGeography)value).Latitude.ToString().Replace(",", ".") + "," + ((DbGeography)value).Longitude.ToString().Replace(",", ".");
                if (s.Length != 0)
                {
                    return s;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }

        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (targetType == typeof(DbGeography))
            {
                if (value != null)
                {
                    var l = value.ToString().Split(',').Reverse();
                    string p = "POINT(" + l.ElementAt(0).ToString() + " " + l.ElementAt(1).ToString() + ")";
                    DbGeography res = DbGeography.PointFromText(p, 4326);
                    return res;
                }
                else return null;
            }
            else return null;
        }
    }
}

