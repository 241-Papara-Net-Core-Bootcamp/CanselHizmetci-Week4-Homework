using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RepositoryPattern.Data.Abstracts;
using RepositoryPattern.Domain.Entities;
using RepositoryPattern.Services.Abstracts;
using RepositoryPattern.Services.DTOs;

namespace RepositoryPattern.Services.Concretes
{
    public class BookService : IBookService
    {
        private readonly IRepository<Book> _repository;
        private readonly IMapper _mapper;
        public BookService(IRepository<Book> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public BookDTO? Get(int id)
        {
            var book = _mapper.Map<BookDTO>(_repository.Get().Include(c=>c.Author).FirstOrDefault(c => c.Id == id));
            return book; 
        }

        public IEnumerable<BookDTO> GetAll()
        {
            var bookList = _mapper.Map<List<BookDTO>>(_repository.Get().Include(c=> c.Author).ToList());
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
