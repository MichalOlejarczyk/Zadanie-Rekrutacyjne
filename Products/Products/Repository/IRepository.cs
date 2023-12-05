namespace Products.Repository
{
    public interface IRepository<T>
        where T : Entity
    {
        bool Add(T entity);
        T? GetById(Guid id);
        bool Update(T entity);

        IEnumerable<T> GetAll(); 
        // Should be also querry but too complicated for now 
    }
}
