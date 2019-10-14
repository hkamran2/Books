﻿using System;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using BooksApi.Filters.ResultFilters;
using BooksApi.Services;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;

namespace BooksApi.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;
        public BooksController(IBookService bookService, IMapper mapper)
        {
            _bookService = bookService;
            _mapper = mapper;
        }

        [HttpGet]
        [ServiceFilter(typeof(BooksCollectionResult))]
        public async Task<IActionResult> GetBooks()
        {
            return Ok(await _bookService.GetBooksAsync());
        }

        [HttpGet]
        [ServiceFilter(typeof(BooksResult))]
        [Route("{id}", Name = "GetBook")]
        public async Task<IActionResult> GetBook(Guid id)
        {
            var book = await _bookService.GetBookAsync(id);

            if (book == null) return NotFound();

            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] BookCreation model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            //Get the id the id that was returned by EF
            var guid = await _bookService.AddBookAsync(model);
            //Get the book that was inserted
            var book = await _bookService.GetBookAsync(guid);

            return CreatedAtRoute("GetBook", new {id = guid}, _mapper.Map<BookDTO>(book));
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> EditBook(Guid id, BookCreation model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _bookService.EditBookAsync(id, model);

            return Ok(model);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            var bookRemoved = await _bookService.RemoveBookAsync(id);

            if (!bookRemoved) return NotFound();

            return Ok();
        }
    }
}