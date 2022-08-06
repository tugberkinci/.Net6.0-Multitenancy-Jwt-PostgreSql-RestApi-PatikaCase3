using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using XSystem.Security.Cryptography;

namespace PatikaHomework3.Helpers
{
    public static class ValidationHelper
    {
        private static string ComputeHash(string input)
        {
            if (input == null) throw new ArgumentNullException("input");

            var hasher = new SHA256Managed();
            var inputBytes = Encoding.UTF8.GetBytes(input);

            var hashBytes = hasher.ComputeHash(inputBytes);
            var hash = new StringBuilder();
            foreach (var b in hashBytes) hash.Append(string.Format("{0:x2}", b));

            return hash.ToString();
        }

        /// <summary>
        /// For password
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetSha(string input)
        {
            return ValidationHelper.ComputeHash(ValidationHelper.ComputeHash(input));
        }


        /// <summary>
        /// Mail validation
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsValidEmail(string email)
        {

            if (String.IsNullOrEmpty(email))
                return false;

            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith(".")) return false;
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Check Phone number
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static bool IsPhoneNumber(string number)
        {

            return Regex.Match(number, @"^(\+[0-9]{10})$").Success;
            
        }

    }
}
