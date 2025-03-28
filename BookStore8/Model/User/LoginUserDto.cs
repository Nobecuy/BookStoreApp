﻿
using System.ComponentModel.DataAnnotations;

namespace BookStore8.Model.User
{
    public class LoginUserDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}