using System.Text.RegularExpressions;

namespace DEX.Core.Helper
{
    public static class DEXHelper
    {
        public static string DecodeID1(string dexData)
        {
            Regex regex = new Regex(@"ID1\*([^*]+)");

            Match match = regex.Match(dexData);

            if (match.Success)
            {
                return match.Groups[1].Value.Split('*')[0];
            }
            else
            {
                return string.Empty;
            }
        }

        public static DateTime DecodeID5(string dexData)
        {
            Regex regex = new Regex(@"ID5\*(\d{8})\*(\d{4})\*(\d{2})");

            Match match = regex.Match(dexData);

            if (match.Success)
            {
                string dateString = match.Groups[1].Value;
                string timeString = match.Groups[2].Value; 
                string secondString = match.Groups[3].Value; 

                if (DateTime.TryParseExact(dateString, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out DateTime dateTime))
                {
                    if (int.TryParse(timeString, out int time))
                    {
                        int hour = time / 100;
                        int minute = time % 100; 

                        if (int.TryParse(secondString, out int second))
                        {
                            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, hour, minute, second);
                        }
                    }
                }
            }

            return default;
        }

        public static decimal DecodeVA1(string dexData)
        {
            Regex regex = new Regex(@"VA1\*([^*]+)");

            Match match = regex.Match(dexData);

            if (match.Success)
            {
                string value = match.Groups[1].Value.Split('*')[0];

                if (decimal.TryParse(value, out decimal result))
                {
                    return result / 100;
                }
                else
                {
                    return 0m;
                }
            }
            else
            {
                return 0m;
            }
        }

        public static (string, decimal) DecodePA1(string dexData)
        {
            Regex regex = new Regex(@"PA1\*([^*]+)\*([^*]+)");

            Match match = regex.Match(dexData);

            if (match.Success)
            {
                string value = match.Groups[2].Value.Split('*')[0];
                if (decimal.TryParse(value, out decimal result))
                {
                     result /= 100;
                }
                else
                {
                    result = 0m;
                }

                return (match.Groups[1].Value, result);
            }

            return (string.Empty, 0m);
        }

        public static (int, decimal) DecodePA2(string dexData)
        {
            Regex regex = new Regex(@"PA2\*([^*]+)\*([^*]+)");

            Match match = regex.Match(dexData);

            if (match.Success)
            {
                string value = match.Groups[2].Value.Split('*')[0];
                if (decimal.TryParse(value, out decimal result))
                {
                    result /= 100;
                }
                else
                {
                    result = 0m;
                }

                return (Convert.ToInt32(match.Groups[1].Value), result);
            }

            return (0, 0m);
        }
    }
}
