﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Models.DTO;

namespace BooksApi.Filters.ResultFilters
{
    public class AuthorsCollectionResult
    {
        private IMapper _mapper;

        public AuthorsCollectionResult(IMapper mapper)
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

            resultFromAction.Value = _mapper.Map<IEnumerable<AuthorDTO>>(
                resultFromAction.Value);

            //Call the next handler
            await next();
        }
    }
}
