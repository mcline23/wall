using System.ComponentModel.DataAnnotations;

namespace wall.Models
{
    public class Message : BaseEntity
    {
        [Required]
        [MinLength(2)]
        public string message { get; set; }

    }








    }