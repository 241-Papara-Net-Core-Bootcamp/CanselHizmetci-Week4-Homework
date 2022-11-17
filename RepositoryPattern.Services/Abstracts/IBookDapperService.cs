using RepositoryPattern.Services.DTOs;

namespace RepositoryPattern.Services.Abstracts
{
    public interface IBookDapperService
    {
        BookDTO? Get(int id);
        IReadOnlyList<BookDTO> GetAll();
        void Add(BookDTO dto);
        void Delete(int id);
        void Update(int id, BookDTO dto);
    }
}
