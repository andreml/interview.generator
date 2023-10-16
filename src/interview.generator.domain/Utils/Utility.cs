namespace interview.generator.domain.Utils
{
    public static class Utility
    {
        public static bool IsCpf(string document)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempdocument;
            string digito;
            int soma;
            int resto;
            document = document.Trim();
            document = document.Replace(".", "").Replace("-", "");
            if (document.Length != 11)
                return false;
            tempdocument = document.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempdocument[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempdocument = tempdocument + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempdocument[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return document.EndsWith(digito);
        }
    }
}
