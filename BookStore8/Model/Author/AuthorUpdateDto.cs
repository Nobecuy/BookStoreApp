﻿using System.ComponentModel.DataAnnotations;

namespace BookStore8.Model.Author
{
    public class AuthorUpdateDto : BaseDto
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(250)]
        public string Bio { get; set; }
    }
}

