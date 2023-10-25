using System.Runtime.CompilerServices;

namespace interview.generator.domain.Utils
{
    public static class RegexUtils
    {
        public const string LoginValidador = @"^[\w.]+$";
        public const string SenhaValidator = @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$";
    }
}
