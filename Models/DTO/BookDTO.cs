﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Models.DTO
{
    public class BookDTO
    {
        public Guid Id { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
