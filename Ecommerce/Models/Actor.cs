using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class Actor
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "profile Pictrue URL")]
        [DataType(DataType.ImageUrl)]
        [Required]
        public string? profilePictrueURL { get; set; }
        [Display(Name = "Full Name")]
        [Required]
        public string? FullName { get; set; }
        [Display(Name = "Biography")]
        [Required]
        public string? Bio { get; set; }

        //relationships:
        public List<Actor_Movie>? Actors_Movies { get; set; }

    }
}
