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
    public class BookService : IBookService
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IRepository<Book> _booksRepository;
        private readonly IMapper _mapper;

        public BookService(IUnitofWork unitofWork, IMapper mapper)
        {
            _unitofWork = unitofWork ?? throw new ArgumentNullException(nameof(unitofWork));
            _mapper = mapper;
            _booksRepository = _unitofWork.GetRepository<Book>() ?? throw new ArgumentNullException(nameof(unitofWork));
        }

        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            return await _booksRepository
                .Get()
                .Include(b => b.Author)
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksAsync(Guid authorID)
        {
            return await _booksRepository
                .Get(b => b.AuthorId == authorID)
                .Include(c => c.Author)
                .ToListAsync();
        }

        public async Task<Book> GetBookAsync(Guid id)
        {
            return await _booksRepository
                .FindAsync(b => b.AuthorId == id);
        }

        public async Task<Book> GetBookAsync(string ISBN)
        {
            return await _booksRepository
                .FindAsync(b => b.ISBN == ISBN);
        }

        public void AddBook(BookCreation book)
        {
            _booksRepository.Add(_mapper.Map<Book>(book));
        }
    }
}