using System.ComponentModel.DataAnnotations;

namespace PatikaHomework3.Dto.Dto
{
    public class AuthenticateRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
