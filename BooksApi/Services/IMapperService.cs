using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace BooksApi.Services
{
    interface IMapperService
    {
        IMapper GetMapper();
    }
    public class MapperService : IMapperService
    {
        private readonly IMapper _mapper;

        public MapperService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IMapper GetMapper()
        {
            return _mapper;
        }
    }
}
