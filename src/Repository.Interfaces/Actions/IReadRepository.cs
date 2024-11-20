namespace Repository.Interfaces.Actions
{
    public interface IReadRepository<T, Y> where T : class
    {
        List<T> GetAll();
        T GetById(Y id);
    }
}
