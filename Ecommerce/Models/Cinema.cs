using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class Cinema
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Cinema Logo")]
        [Required]
        public string CinmaLogo { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Discription { get; set; }

        //relationships:
        public List<Movie>? Movies { get; set; }

    }
}
