using System.ComponentModel.DataAnnotations;

namespace PatikaHomework3.Data.Model
{
    public class Account
    {
        //public Account()
        //{
        //    this.People = new HashSet<Person>(); 
        //}
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public DateTime LastActivity { get; set; }

        //nav
        public ICollection<Person> Person { get; set; }
    }
}
