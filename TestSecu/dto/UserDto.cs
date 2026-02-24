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
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{10,}$",
            ErrorMessage = "Password must be at least 10 characters and include at least 1 lowercase letter, 1 uppercase letter, 1 digit, and 1 special character.")]
        public required string Password { get; set; }
    }
}
