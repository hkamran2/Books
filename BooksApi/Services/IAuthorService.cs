using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.DTO;
using Models.Entities;

namespace BooksApi.Services
{
    public interface IAuthorService
    {
        Task<IEnumerable<Author>> GetAuthorsAsync();
        Task<Author> GetAuthorAsync(Guid id);
        Task<Guid> AddAuthorAsync(AuthorDTO author);
        Task<bool> RemoveAuthorAsync(Guid id);
    }
}
