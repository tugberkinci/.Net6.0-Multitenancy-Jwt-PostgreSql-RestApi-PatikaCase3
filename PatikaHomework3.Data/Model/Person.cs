using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatikaHomework3.Data.Model
{

    public class Person
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("AccountId")]
        public int AccountId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }

        //nav

        public Account Account { get; set; }
    }
}
