﻿using System.ComponentModel.DataAnnotations.Schema;

namespace SharedRecipe.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Password { get; set; }
    }
}
