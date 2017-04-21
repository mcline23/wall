using System.ComponentModel.DataAnnotations;

namespace wall.Models
{

public class Comment: BaseEntity{
    [MinLengthAttribute(2)]
    public string comment{ get; set; }
    
}

}