namespace CodeGroupTeste.Infra.Interfaces;

public interface IRepositoryBase<T> where T : class
{
    Task<List<T>> GetAll();
    Task<T> GetById(Guid id);
    Task<T> Create(T entity);
    Task<T> Update(T entity);
    Task<bool> Delete(Guid id);
}
