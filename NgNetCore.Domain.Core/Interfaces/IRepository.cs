public interface IRepository<T>
{
    T Create(T entity);

    List<T> GetList();

    T GetById(int id);

    int Update(T entity);

    int Delete(int id);

}
