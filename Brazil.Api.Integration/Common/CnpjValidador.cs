using System.Text.RegularExpressions;

namespace Brazil.Api.Integration.Common
{
    public static class CnpjValidador
    {
        private static readonly int TAMANHO_CNPJ_SEM_DV = 12;
        private static readonly string REGEX_CARACTERES_FORMATACAO = "[./-]";
        private static readonly string REGEX_FORMACAO_BASE_CNPJ = "[A-Z\\d]{12}";
        private static readonly string REGEX_FORMACAO_DV = "[\\d]{2}";
        private static readonly string REGEX_VALOR_ZERADO = "^[0]+$";

        private static readonly int VALOR_BASE = (int)'0';
        private static readonly int[] PESOS_DV = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        public static bool IsValid(this string cnpj)
        {
            if (cnpj != null)
            {
                cnpj = RemoveCaracteresFormatacao(cnpj);
                if (IsCnpjFormacaoValidaComDV(cnpj))
                {
                    string dvInformado = cnpj.Substring(TAMANHO_CNPJ_SEM_DV);
                    string dvCalculado = CalculaDV(cnpj.Substring(0, TAMANHO_CNPJ_SEM_DV));
                    return dvCalculado.Equals(dvInformado);
                }
            }

            return false;
        }

        public static string CalculaDV(this string baseCnpj)
        {
            if (baseCnpj != null)
            {
                baseCnpj = RemoveCaracteresFormatacao(baseCnpj);

                if (IsCnpjFormacaoValidaSemDV(baseCnpj))
                {

                    string dv1 = string.Format("{0}", CalculaDigito(baseCnpj));
                    string dv2 = string.Format("{0}", CalculaDigito(baseCnpj + dv1));
                    return dv1 + dv2;

                }
            }

            throw new Exception($"Cnpj {baseCnpj} não é válido para o cálculo do DV");
        }

        private static int CalculaDigito(string cnpj)
        {
            int soma = 0;
            for (int indice = cnpj.Length - 1; indice >= 0; indice--)
            {
                int valorCaracter = (int)cnpj[indice] - VALOR_BASE;
                soma += valorCaracter * PESOS_DV[PESOS_DV.Length - cnpj.Length + indice];
            }
            return soma % 11 < 2 ? 0 : 11 - (soma % 11);
        }

        private static string RemoveCaracteresFormatacao(string cnpj)
        {
            return cnpj.Trim().Replace(REGEX_CARACTERES_FORMATACAO, "");
        }

        private static bool IsCnpjFormacaoValidaSemDV(string cnpj)
        {
            return Regex.IsMatch(cnpj, REGEX_FORMACAO_BASE_CNPJ)
                   && !Regex.IsMatch(cnpj, REGEX_VALOR_ZERADO);
        }

        private static bool IsCnpjFormacaoValidaComDV(string cnpj)
        {
            return Regex.IsMatch(cnpj, REGEX_FORMACAO_BASE_CNPJ + REGEX_FORMACAO_DV)
                   && !Regex.IsMatch(cnpj, REGEX_VALOR_ZERADO);
        }
    }
}
