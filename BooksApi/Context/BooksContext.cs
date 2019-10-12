﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace BooksApi.Context
{
    public class BooksContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        public BooksContext(DbContextOptions<BooksContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // seed the database with dummy data
            AuthorDataSeeder.AuthorDataSeed(modelBuilder);

            // seed the database with dummy data
            BooksDataSeeder.SeedBookData(modelBuilder);
            //Make the ISBN number unique
            modelBuilder.Entity<Book>(b => b.HasKey(k => new {k.Id, k.ISBN}));

            base.OnModelCreating(modelBuilder);
        }
    }
}
