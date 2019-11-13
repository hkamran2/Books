using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ModelLibrary.DTO;
using ModelLibrary.Entities;

namespace BooksApi.Profiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookDTO>()
                .ForMember(b => b.Author, opt => opt.MapFrom(a => $"{a.Author.FirstName} {a.Author.LastName}"));

            CreateMap<BookCreation, Book>()
                .ForMember(b => b.Id, opt => opt.Ignore());
        }
    }
}
