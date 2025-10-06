namespace RecipeManager.Storage;

public interface IStorage<T>
{
    bool Add(T entity);
    bool Remove(string name);
    bool Update(T entity);
    T? GetEntityByName(string name);
    int GetTotalQuantity();
    List<T> GetAll();
    bool ExistsByName(string name);
    void AddAll(IEnumerable<T> items);
    void RemoveAll(); 
} 