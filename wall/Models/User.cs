using System.ComponentModel.DataAnnotations;

namespace wall.Models
{
    public class User : BaseEntity
    {
       
        [Required]
        [MinLength(4)]
        public string FirstName { get; set; }
        [Required]
        [MinLength(4)]
        public string LastName { get; set; }
       
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLengthAttribute(8)]
        public string Password { get; set; }

        
        [Compare("Password", ErrorMessage = "Your passwords don't match!")]
        public string Passwordconfirm { get; set; }
    }
   
}


    