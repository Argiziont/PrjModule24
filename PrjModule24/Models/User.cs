﻿using System.ComponentModel.DataAnnotations;
using PrjModule24.Models.Shared;

namespace PrjModule24.Models
{
    public record User : Human
    {
        [MinLength(6)]
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        [Required] public Role Role { get; set; }
        [Required] public Account Account { get; set; }

        [Key] public string Id { get; set; }
    }
}