public sealed class EntityProvider
{
    public IEntity Value { get; }
        
    public EntityProvider(IEntity value)
    {
        Value = value;
    }
}