public interface IPoolable<T>
{
    void InitPoolable(System.Action<T> action);
    void ReturnToPool();
}
