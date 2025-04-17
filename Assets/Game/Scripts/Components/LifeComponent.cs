using System;
using UnityEngine;

public sealed class LifeComponent : IDamageable
{    
    public event Action OnEmpty;
    public event Action OnLifeChanged;
        
    private readonly int _maxPoints;        
    private int _hitPoints;
    private bool _isDead = false;

    public LifeComponent(int maxPoints, int hitPoints)
    {
        _maxPoints = maxPoints;
        _hitPoints = hitPoints;   
    }

    public void TakeDamage(int damage)
    {        
        _hitPoints -= damage;
        OnLifeChanged?.Invoke();

        Debug.Log($"Current HP: {_hitPoints}");

        if (_hitPoints <= 0)
        {
            _isDead = true;
            this.OnEmpty?.Invoke();
        }
    }

    public bool IsAlive()
    {
        return !_isDead;
    }
}
