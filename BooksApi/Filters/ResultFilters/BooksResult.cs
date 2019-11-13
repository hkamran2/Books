using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ModelLibrary.DTO;

namespace BooksApi.Filters.ResultFilters
{
    public class BooksResult : IAsyncResultFilter
    {
        private readonly IMapper _mapper;

        public BooksResult(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var resultFromAction = context.Result as ObjectResult;
            if (resultFromAction?.Value == null
                || resultFromAction.StatusCode < 200
                || resultFromAction.StatusCode >= 300)
            {
                //Call the next handler
                await next();
                return;
            }

            resultFromAction.Value = _mapper.Map<BookDTO>(
                resultFromAction.Value);

            //Call the next handler
            await next();
        }
    }
}
