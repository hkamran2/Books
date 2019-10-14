using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BooksApi.Filters.ResultFilters;
using BooksApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BooksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private IAuthorService _service;

        public AuthorsController(IAuthorService service)
        {
            _service = service;
        }

        [ServiceFilter(typeof(AuthorsCollectionResult))]
        public async Task<IActionResult> GetAuthors()
        {
            return Ok(await _service.GetAuthorsAsync());
        }

    }
}