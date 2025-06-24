using System.ComponentModel.DataAnnotations;

namespace UaddAPI.Dto.Match
{
    public class MatchCreateDto
    {
        [Required, MaxLength(150)]
        public string Title { get; set; } = null!;

        [Required, MaxLength(100)]
        public string TeamA { get; set; } = null!;

        [MaxLength(500)]
        public string DescriptionA { get; set; } = null!;

        [Required, MaxLength(100)]
        public string TeamB { get; set; } = null!;

        [MaxLength(500)]
        public string DescriptionB { get; set; } = null!;

        [Required]
        public DateTime MatchDate { get; set; }

        [MaxLength(200)]
        public string Location { get; set; } = null!;

        [Required, MaxLength(100)]
        public string SportType { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;

        [Required, MaxLength(150)]
        public string ChampionshipName { get; set; } = null!;
    }
}