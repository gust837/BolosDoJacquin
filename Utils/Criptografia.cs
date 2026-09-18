using System;

namespace BolosDoJacquin.Utils
{
    public static class Criptografia
    {
        public static string GerarHash(string senhaPura)
        {
            return BCrypt.Net.BCrypt.HashPassword(senhaPura, 12);
        }

        public static bool CompararHash(string senhaPura, string senhaHash)
        {
            try
            {
                return BCrypt.Net.BCrypt.Verify(senhaPura, senhaHash);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
