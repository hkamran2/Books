using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BooksApi.Repository;
using Microsoft.EntityFrameworkCore;
using Models.DTO;
using Models.Entities;

namespace BooksApi.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IMapper _mapper;
        private readonly IRepository<Author> _repository;

        public AuthorService(IUnitofWork unitofWork, IMapper mapper)
        {
            _unitofWork = unitofWork;
            _mapper = mapper;
            _repository = _unitofWork.GetRepository<Author>();
        }
        public async Task<IEnumerable<Author>> GetAuthorsAsync()
        {
            return await _repository.Get().ToArrayAsync();
        }

        public async Task<Author> GetAuthorAsync(Guid id)
        {
            return await _repository
                .Get(a => a.Id == id)
                .SingleOrDefaultAsync();
        }

        public async Task<Guid> AddAuthorAsync(AuthorCreation model)
        {
            var author = _mapper.Map<Author>(model);

            _repository.Add(author);

            await _unitofWork.SaveChangesAsync();

            return author.Id;
        }

        public async Task<bool> RemoveAuthorAsync(Guid id)
        {
            var author = await GetAuthorAsync(id);

            if (author == null) return false;

            _repository.Delete(author);

            return await _unitofWork.SaveChangesAsync();
        }

        public async Task<Author> EditAuthorAsync(Guid id, AuthorCreation model)
        {
            var author = await GetAuthorAsync(id);
            if (author != null)
            {
                author.FirstName = model.FirstName;
                author.LastName = model.LastName;
            }

            return author;
        }
    }
}