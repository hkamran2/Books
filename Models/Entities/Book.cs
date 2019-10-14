﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Text;

namespace Models.Entities
{
        [Table("Books")]
        public class Book
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
            public string ISBN { get; set; }

            [Required]
            [MaxLength(150)]
            public string Title { get; set; }

            [MaxLength(2500)]
            public string Description { get; set; }

            public Guid AuthorId { get; set; }
            public Author Author { get; set; }
        }
    }
