using System.Text.RegularExpressions;

namespace Brazil.Api.Integration.Common
{
    public static class CpfCnpjValidate
    {
        public static string CpfCnpjIsValid(this string cnpj)
        {
            if (!Regex.IsMatch(cnpj, @"^[0-9]+$"))
                return "Please enter numbers only";

            if (cnpj.Length < 14 || cnpj.Length > 14)
                return "Please enter a Cnpj with 14 digits";

            return string.Empty;
        }
    }
}
