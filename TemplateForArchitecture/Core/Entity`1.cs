namespace Core
{
    public class Entity<TKey> : Entity
    {
        public TKey Id { get; set; }
    }
}
