using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.DTO;
using Models.Entities;

namespace BooksApi.Services
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetBooksAsync();
        Task<IEnumerable<Book>> GetBooksAsync(Guid authorID);
        Task<Book> GetBookAsync(Guid id);

        Task<Book> GetBookAsync(string ISBN);
        void  AddBook(BookCreation book);
    }
}
