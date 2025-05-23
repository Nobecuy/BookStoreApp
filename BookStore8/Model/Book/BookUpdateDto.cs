﻿using System.ComponentModel.DataAnnotations;

namespace BookStore8.Model.Book
{
    public class BookUpdateDto : BaseDto
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [Range(1800, int.MaxValue)]
        public int? Year { get; set; }
        [Required]
        public string Isbn { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 10)]
        public string? Summary { get; set; }
        public string? Image { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public decimal? Price { get; set; }
    }
}
