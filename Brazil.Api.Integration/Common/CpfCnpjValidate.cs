namespace Brazil.Api.Integration.Common
{
    public static class CpfCnpjValidate
    {
        public static string CpfCnpjIsValid(this string cnpj)
        {
            if (cnpj.Length < 5 || cnpj.Length > 14)
                return "Please enter a valid Cnpj";

            return string.Empty;
        }
    }
}
