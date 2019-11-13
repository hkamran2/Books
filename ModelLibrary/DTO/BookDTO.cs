﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLibrary.DTO
{
    public class BookDTO
    {
        public Guid Id { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
