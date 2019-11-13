using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ModelLibrary.DTO;

namespace Books_Client.Http
{
    public interface IHttpAction<T>
    {
        Task<IEnumerable<T>> GetCollection(string uri);
        Task<T> GetObject(string uri);
    }
}
