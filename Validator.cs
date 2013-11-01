using System;
using System.Text.RegularExpressions;

namespace SimpleProjectManagement
{
    public static class Validator
    {
        public static bool EmptyText(string text)
        {
            return string.IsNullOrWhiteSpace(text);
        }

        public static bool TextIsNumber(string text)
        {
            int temp;

            return int.TryParse(text, out temp);
        }

        public static bool TextIsDate(string text)
        {
            DateTime temp;

            if (!DateTime.TryParse(text, out temp))
            {
                return false;
            }

            DateTime min = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue; // 1.1.1753
            DateTime max = (DateTime)System.Data.SqlTypes.SqlDateTime.MaxValue; // 31.12.9999

            // Error - SqlDateTime overflow. Must be between 1/1/1753 12:00:00 AM and 12/31/9999 11:59:59 PM
            // http://stackoverflow.com/questions/468045/error-sqldatetime-overflow-must-be-between-1-1-1753-120000-am-and-12-31-999
            if (Convert.ToDateTime(text).Date < min.Date || Convert.ToDateTime(text).Date > max)
            {
                return false;
            }
            return true;
        }

        public static T ConvertFromDBVal<T>(object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return default(T);
            }
            else
            {
                return (T)obj;
            }
        }

        public static bool ValidEmail(string email)
        {
            if (Regex.Match(email,
               @"[a-zA-Z0-9]([a-zA-Z0-9_\-\.]*)[a-zA-Z0-9]@[a-zA-Z0-9]([a-zA-Z0-9_\-\.]*)(\.[a-zA-Z]{2,4})").Success)
            {
                return true;
            }

            return false;
        }

        /*
          Za Hr telefon: http://www.porezna-uprava.hr/e-Porezna/IP/ip-obrazac_4_0.xsd
          Tip za definiranje formata telefonskih brojeva:
          1. obvezno pocinje s +
          2. slijedi pozivni broj za drzavu ( jedna do tri znamenke ), broj mora biti veci od nule
          3. otvorena zagrada (
          4. pozivni broj za grad ( jedno ili dvoznamenkasti broj, ne moze poceti s nulom )
          5. zatvorena zagrada )
          6. sestero ili sedmeroznamenkasti broj, ne moze poceti s nulom
          Primjeri: +385(1)1234567, +385(91)123456
        */
        public static bool ValidPhoneNumber(string phone)
        {
            if (Regex.Match(phone, @"\+((0?0?)[1-9]|(0?[1-9]\d)|[1-9]\d{2})\([1-9]\d?\)[1-9]\d{5,6}").Success)
                return true;

            return false;
        }
    }
}