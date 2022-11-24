public interface IPoolable<T>
{
    void Init(System.Action<T> action);
    void ReturnToPool();
}
