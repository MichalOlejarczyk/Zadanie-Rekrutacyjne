
namespace Products.Repository
{
    public class InMemoryDb<T> : IRepository<T>
        where T : Entity
    {
        private readonly Dictionary<Guid, T> _db; // better to use Concurrent dictionary

        public InMemoryDb()
        {
            _db = new Dictionary<Guid, T>();
        }

        public bool Add(T entity)
        {
            if (entity == null || _db.ContainsKey(entity.Id))
                return false;
            _db.Add(entity.Id, entity);
            return true;
        }

        public IEnumerable<T> GetAll()
        {
            return _db.Values;
        }

        public T GetById(Guid id)
        {
            return _db[id];
        }

        public bool Update(T entity)
        {
            if (entity == null || !_db.ContainsKey(entity.Id))
                return false;
            _db[entity.Id] = entity;
            return true;
        }
    }
}
