namespace LudusGestao.Domain.Common.Constants
{
    public static class JwtConstants
    {
        public const string Issuer = "SistemaReservasIssuer";
        public const string Audience = "SistemaReservasAudience";
        public const string Key = "ludus-sistemas-chave-super-secreta-2024";
        public const int AccessTokenExpirationHours = 2;
        public const int RefreshTokenExpirationDays = 30;
    }
}
