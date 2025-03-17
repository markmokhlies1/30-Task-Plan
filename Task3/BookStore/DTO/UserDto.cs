using System.ComponentModel.DataAnnotations;

namespace BookStore.DTO
{
    public class UserDto
    {
        [Required]
        [RegularExpression("^[a-zA-Z0-9-_]+$", ErrorMessage = "Username can only contain letters, numbers, dashes, and underscores.")]
        public string UserName {  get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
