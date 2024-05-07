namespace InterviewGenerator.Domain.Utils;

public static class RegexUtils
{
    public const string LoginValidador = @"^[\w.]+$";
    public const string SenhaValidator = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[$*&@#])[0-9a-zA-Z$*&@#]{8,}$";
}
