using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Ecommerce.Data.Enums;

namespace Ecommerce.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Movie Name")]
        [Required]
        public string Name { get; set; }
        [Required]
        public string Discription { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public double Price { get; set; }
        [Required]
        [DataType(DataType.ImageUrl)]
        [Display(Name = "Movie Picture")]
        public string ImageURL { get; set; }
        [Required]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [Required]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
        [Required]
        [Display(Name = "Movie Category")]
        public MovieCategory MovieCategory { get; set; }

        //Relationships:

        public List<Actor_Movie>? Actors_Movies { get; set; }

        //cinema
        public int CinemaID { get; set; }
        [ForeignKey("CinemaID")]
        public List<Cinema>? Cinemas { get; set; }

        //Producer
        public int ProducerID { get; set; }
        [ForeignKey("ProducerID")]
        public List<Producer>? Producers { get; set; }
    }
}
