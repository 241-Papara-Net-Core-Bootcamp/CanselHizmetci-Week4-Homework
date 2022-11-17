using RepositoryPattern.Domain.Entities;
using RepositoryPattern.Services.DTOs;

namespace RepositoryPattern.Services.Abstracts
{
    public interface IAuthorDapperService
    {
        AuthorDTO? Get(int id);
        IReadOnlyList<AuthorDTO> GetAll();
        void Add(AuthorDTO dto);
        void Delete(int id);
        void Update(int id, AuthorDTO dto);
    }
}
