﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.DTO
{
    public class AuthorDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
