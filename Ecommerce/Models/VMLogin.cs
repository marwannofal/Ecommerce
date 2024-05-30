using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class VMLogin
    {
        [Key]
        public int id { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool KeepLoggedIn { get; set; }

    }
}
