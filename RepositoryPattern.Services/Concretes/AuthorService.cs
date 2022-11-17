using AutoMapper;
using RepositoryPattern.Data.Abstracts;
using RepositoryPattern.Domain.Entities;
using RepositoryPattern.Services.Abstracts;
using RepositoryPattern.Services.DTOs;

namespace RepositoryPattern.Services.Concretes
{
    public class AuthorService : IAuthorService
    {
        private readonly IRepository<Author> _repository;
        private readonly IMapper _mapper;

        public AuthorService(IRepository<Author> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public AuthorDTO Get(int id)
        {
            return _mapper.Map<AuthorDTO>(_repository.Get().FirstOrDefault(c => c.Id == id));

        }

        public IEnumerable<AuthorDTO> GetAll()
        {
            var authorList = _mapper.Map<List<AuthorDTO>>(_repository.Get().ToList());
            return authorList;
        }

        //public void Add(AuthorDTO dto)
        //{
        //    throw new NotImplementedException();
        //}

        public void Add(AuthorDTO author)
        {
            _repository.Add(_mapper.Map<Author>(author));
           
        }

        public void Delete(int id)
        {
            _repository.Remove(id);
        }

        public void Update(int id, AuthorDTO author)
        {
            _repository.Update(id, _mapper.Map<Author>(author));
        }
    }
}
