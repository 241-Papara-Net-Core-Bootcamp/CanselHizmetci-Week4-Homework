using AutoMapper;
using RepositoryPattern.Data.Abstracts;
using RepositoryPattern.Domain.Entities;
using RepositoryPattern.Services.Abstracts;
using RepositoryPattern.Services.DTOs;

namespace RepositoryPattern.Services.Concretes
{
    public class BookDapperService : IBookDapperService
    {
        private readonly IDapperRepository<Book> _repository;
        private readonly IMapper _mapper;

        public BookDapperService(IDapperRepository<Book> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public BookDTO? Get(int id)
        {
            return _mapper.Map<BookDTO>(_repository.Get(id));

        }

        public IReadOnlyList<BookDTO> GetAll()
        {
            var bookList = _mapper.Map<List<BookDTO>>(_repository.GetAll());
            return bookList;
        }

        public void Add(BookDTO book)
        {
            _repository.Add(_mapper.Map<Book>(book));

        }

        public void Delete(int id)
        {
            _repository.Remove(id);
        }

        public void Update(int id, BookDTO book)
        {
            _repository.Update(id, _mapper.Map<Book>(book));

        }
    }
}
