using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Books_Client.Http
{
    public class HttpAction<T> : IHttpAction<T> where T:class
    {
        private readonly IHttpClientFactory _client;

        public HttpAction(IHttpClientFactory _client)
        {
            this._client = _client;
        }
        public async Task<IEnumerable<T>> GetCollection(string uri)
        {
            var client = _client.CreateClient("books-api");
            var req = new HttpRequestMessage(HttpMethod.Get, uri);
            var res = await client.SendAsync(req);
            //return the object as the type defined
            return await res.Content.ReadAsAsync<IEnumerable<T>>();
        }

        public async Task<T> GetObject(string uri)
        {
            var client = _client.CreateClient("books-api");
            var req = new HttpRequestMessage(HttpMethod.Get, uri);
            var res = await client.SendAsync(req);
            return await res.Content.ReadAsAsync<T>();
        }
    }
}