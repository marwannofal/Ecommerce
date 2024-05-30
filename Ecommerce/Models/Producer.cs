using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class Producer
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Profile Picture URL")]
        [Required]
        public string ProfilePictureURL { get; set; }
        [Display(Name = "Full Name")]
        [Required]
        public string FullName { get; set; }
        [Display(Name = "Biography")]
        [Required]
        public string? Bio { get; set; }


        //relationships:
        public List<Movie>? Movies { get; set; }
    }
}
