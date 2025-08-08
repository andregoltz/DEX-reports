using DEX.Core.DTO.Request;

namespace DEX.Core.Helper
{
    public static class DEXHelper
    {
        public static DexData ParseDexFile(string dexData)
        {
            var result = new DexData();

            int pos = 0;
            while (pos < dexData.Length)
            {
                // ID1*value
                int idxID1 = dexData.IndexOf("ID1*", pos, StringComparison.OrdinalIgnoreCase);
                if (idxID1 != -1 && idxID1 == pos)
                {
                    int start = idxID1 + 4;
                    int end = dexData.IndexOf('*', start);
                    if (end == -1) end = dexData.Length;
                    result.ID1 = dexData.Substring(start, end - start);
                    pos = end;
                    continue;
                }

                // ID5*yyyyMMdd*HHmm*ss
                int idxID5 = dexData.IndexOf("ID5*", pos, StringComparison.OrdinalIgnoreCase);
                if (idxID5 != -1 && idxID5 == pos)
                {
                    int start = idxID5 + 4;
                    int firstStar = dexData.IndexOf('*', start);
                    int secondStar = -1;
                    if (firstStar != -1)
                        secondStar = dexData.IndexOf('*', firstStar + 1);

                    if (firstStar != -1 && secondStar != -1)
                    {
                        string dateStr = dexData.Substring(start, firstStar - start);
                        string timeStr = dexData.Substring(firstStar + 1, secondStar - firstStar - 1);
                        string secondStr = string.Empty;

                        int thirdStar = dexData.IndexOf('*', secondStar + 1);
                        if (thirdStar == -1)
                            secondStr = dexData.Substring(secondStar + 1);
                        else
                            secondStr = dexData.Substring(secondStar + 1, thirdStar - secondStar - 1);

                        if (DateTime.TryParseExact(dateStr, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out DateTime dateTime)
                            && int.TryParse(timeStr, out int time)
                            && int.TryParse(secondStr, out int second))
                        {
                            int hour = time / 100;
                            int minute = time % 100;
                            result.ID5 = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, hour, minute, second);
                        }
                    }
                    pos = idxID5 + 4;
                    continue;
                }

                // VA1*value
                int idxVA1 = dexData.IndexOf("VA1*", pos, StringComparison.OrdinalIgnoreCase);
                if (idxVA1 != -1 && idxVA1 == pos)
                {
                    int start = idxVA1 + 4;
                    int end = dexData.IndexOf('*', start);
                    if (end == -1) end = dexData.Length;
                    string valStr = dexData.Substring(start, end - start);
                    if (decimal.TryParse(valStr, out var val))
                        result.VA1 = val / 100m;
                    pos = end;
                    continue;
                }

                // PA1*productIdentifier*price
                int idxPA1 = dexData.IndexOf("PA1*", pos, StringComparison.OrdinalIgnoreCase);
                if (idxPA1 != -1 && idxPA1 == pos)
                {
                    int start = idxPA1 + 4;
                    int firstStar = dexData.IndexOf('*', start);
                    if (firstStar == -1)
                    {
                        pos += 4;
                        continue;
                    }
                    string productIdentifier = dexData.Substring(start, firstStar - start);

                    int secondStar = dexData.IndexOf('*', firstStar + 1);
                    string priceStr;
                    if (secondStar == -1)
                        priceStr = dexData.Substring(firstStar + 1);
                    else
                        priceStr = dexData.Substring(firstStar + 1, secondStar - firstStar - 1);

                    decimal price = decimal.TryParse(priceStr, out var p) ? p / 100m : 0m;

                    int idxPA2 = dexData.IndexOf("PA2*", idxPA1, StringComparison.OrdinalIgnoreCase);
                    if (idxPA2 != -1)
                    {
                        int startPA2 = idxPA2 + 4;
                        int star1 = dexData.IndexOf('*', startPA2);
                        int star2 = -1;
                        if (star1 != -1)
                            star2 = dexData.IndexOf('*', star1 + 1);

                        if (star1 != -1)
                        {
                            string numberOfVendsStr = dexData.Substring(startPA2, star1 - startPA2);
                            string valueOfPaidSalesStr;
                            if (star2 == -1)
                                valueOfPaidSalesStr = dexData.Substring(star1 + 1);
                            else
                                valueOfPaidSalesStr = dexData.Substring(star1 + 1, star2 - star1 - 1);

                            int numberOfVends = int.TryParse(numberOfVendsStr, out var nv) ? nv : 0;
                            decimal valueOfPaidSales = decimal.TryParse(valueOfPaidSalesStr, out var vps) ? vps / 100m : 0m;

                            result.PARecords.Add((productIdentifier, price, numberOfVends, valueOfPaidSales));
                        }
                    }
                    pos = idxPA1 + 4;
                    continue;
                }
                pos++;
            }
            return result;
        }
    }
}
