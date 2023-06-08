using System.Text.RegularExpressions;

namespace Brazil.Api.Integration.Common
{
    public static class OnlyNumbersValidate
    {
        public static string OnlyNumbersIsValid(this string value)
        {
            if (!Regex.IsMatch(value, @"^[0-9]+$"))
                return "Please enter numbers only";

            return string.Empty;
        }
    }
}
