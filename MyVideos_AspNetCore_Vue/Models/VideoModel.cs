using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class VideoModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Range(1900, 2100)]
        public int YearOfProduction { get; set; }
    }
}
