using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModelLibrary.DTO;
using ModelLibrary.Entities;

namespace BooksApi.Services
{
    public interface IAuthorService
    {
        Task<IEnumerable<Author>> GetAuthorsAsync();
        Task<Author> GetAuthorAsync(Guid id);
        Task<Guid> AddAuthorAsync(AuthorCreation author);
        Task<bool> RemoveAuthorAsync(Guid id);
        Task<Author> EditAuthorAsync(Guid id, AuthorCreation model);
    }
}
