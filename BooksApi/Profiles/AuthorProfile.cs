﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Models.DTO;
using Models.Entities;

namespace BooksApi.Profiles
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<Author, AuthorDTO>()
                .ForMember(a => a.Name, opt => opt.MapFrom(a => $"{a.FirstName} {a.LastName}"));

            CreateMap<AuthorCreation, Author>();
        }
    }
}
