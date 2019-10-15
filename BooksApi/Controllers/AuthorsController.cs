using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BooksApi.Filters.ResultFilters;
using BooksApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;

namespace BooksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _service;

        public AuthorsController(IAuthorService service)
        {
            _service = service;
        }

        [HttpGet]
        [ServiceFilter(typeof(AuthorsCollectionResult))]
        public async Task<IActionResult> GetAuthors()
        {
            return Ok(await _service.GetAuthorsAsync());
        }

        [ServiceFilter(typeof(AuthorResult))]
        [Route("{id}", Name = "GetAuthor")]
        public async Task<IActionResult> GetAuthor(Guid id)
        {
            var author = await _service.GetAuthorAsync(id);

            if (author == null) return NotFound();

            return Ok(author);
        }

        [HttpPost]
        [ServiceFilter(typeof(AuthorResult))]
        public async Task<IActionResult> AddAuthor([FromBody] AuthorCreation model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var id = await _service.AddAuthorAsync(model);

            return CreatedAtRoute("GetAuthor", new {id = id}, await _service.GetAuthorAsync(id));
        }

        [HttpPut]
        [Route("{id}")]
        [ServiceFilter(typeof(AuthorResult))]
        public async Task<IActionResult> EditAuthor(Guid id, AuthorCreation model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var author = await _service.EditAuthorAsync(id, model);

            if (author == null) return NotFound();

            return Ok(author);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAuthor(Guid id)
        {
            var authorDeleted = await _service.RemoveAuthorAsync(id);

            if (authorDeleted) return Ok();

            return NotFound();
        }
    }
}