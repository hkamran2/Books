using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BooksApi.Repository;
using Microsoft.EntityFrameworkCore;
using ModelLibrary.DTO;
using ModelLibrary.Entities;

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
            return await _booksRepository.Get(b => b.Id == id)
                .Include(b => b.Author)
                .SingleOrDefaultAsync();
        }
        
        public async Task<Book> GetBookAsync(string ISBN)
        {
            return await _booksRepository
                .Get(b => b.ISBN == ISBN)
                .Include(b => b.Author)
                .SingleOrDefaultAsync();
        }

        public async Task<Guid> AddBookAsync(BookCreation book)
        {
            var bookToCreate = _mapper.Map<Book>(book);

            _booksRepository.Add(bookToCreate);

            await _unitofWork.SaveChangesAsync();

            //Return id for the controller action to pass
            //in the id into for the location header
            return bookToCreate.Id;
        }

        public async Task<Book> EditBookAsync(Guid id, BookCreation model)
        {
            var book = await GetBookAsync(id);
            if (book != null)
            {
                //Make changes to the book
                book.AuthorId = model.AuthorId;
                book.ISBN = model.ISBN;
                book.Description = model.Description;
                book.Title = model.Title;
            }
            
            await _unitofWork.SaveChangesAsync();

            return book;
        }

        public async Task<bool> RemoveBookAsync(Guid id)
        {
            var book = await GetBookAsync(id);
            //if the book is not found then return false
            if (book == null) return false;
            _booksRepository.Delete(book);
            return await _unitofWork.SaveChangesAsync();
        }
    }
}