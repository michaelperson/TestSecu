using System.ComponentModel.DataAnnotations;

namespace TestSecu.dto
{
    public class UserRequestDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; }

        [Required]
        [MaxLength(100)]
        [MinLength(10)]
        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$")]
        
        public required string Password { get; set; }
    }
}
