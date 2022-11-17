using System.Linq.Expressions;

namespace RepositoryPattern.Data.Abstracts
{
    public interface IDapperRepository<T> where T : class
    {
        public IReadOnlyList<T> GetAll();
        public T Get(int id);
        public void Add(T entity);
        public void Remove(int id);  
        public void Update(int id, T entity);
    }
}
