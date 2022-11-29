public interface IOrderUnitSpawn
{
    public void OrderSpawn();
    public void Register(ISpawner spawner);
    public void Deregister(ISpawner spawner);
}
