using Zenject;

public sealed class CharacterConditionObserver : IInitializable
{
    private readonly EntityProvider _character;
    private LifeComponent _lifeComponent;
    private MoveComponent _moveComponent;
    private JumpComponent _jumpComponent;
    private RotateComponent _rotateComponent;
    private GroundComponent _groundComponent;
    private PushComponent _pushComponent;
    private TossComponent _tossComponent;   

    public CharacterConditionObserver(EntityProvider character)
    {
        _character = character;
    }        

    void IInitializable.Initialize()
    {
        _lifeComponent = _character.Value.Get<LifeComponent>();
        _moveComponent = _character.Value.Get<MoveComponent>();
        _jumpComponent = _character.Value.Get<JumpComponent>();
        _rotateComponent = _character.Value.Get<RotateComponent>();
        _groundComponent = _character.Value.Get<GroundComponent>();
        _pushComponent = _character.Value.Get<PushComponent>();
        _tossComponent = _character.Value.Get<TossComponent>();        

        _moveComponent.AddCondition(_lifeComponent.IsAlive);        

        _rotateComponent.AddCondition(_lifeComponent.IsAlive);

        _jumpComponent.AddCondition(_lifeComponent.IsAlive);
        _jumpComponent.AddCondition(_groundComponent.GetGroundState);        

        _pushComponent.AddCondition(_lifeComponent.IsAlive);

        _tossComponent.AddCondition(_lifeComponent.IsAlive);
        _tossComponent.AddCondition(_groundComponent.GetGroundState);
    }
}
