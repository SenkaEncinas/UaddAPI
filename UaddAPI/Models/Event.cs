using System;
using System.ComponentModel.DataAnnotations;

namespace UaddAPI.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(150)]
        public string Title { get; set; } = null!;

        [MaxLength(500)]
        public string Description { get; set; } = null!;

        [Required]
        public DateTime Date { get; set; }

        [MaxLength(200)]
        public string Location { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;

        [Required]
        public DateTime PublishDate { get; set; }

        [Required]
        public int CreatedByUserId { get; set; }

        // NUEVOS CAMPOS

        [MaxLength(100)]
        public string Carrera { get; set; } = null!;

        [MaxLength(300)]
        public string FormLink { get; set; } = null!;

        [MaxLength(300)]
        public string Ubicacion { get; set; } = null!;
    }
}