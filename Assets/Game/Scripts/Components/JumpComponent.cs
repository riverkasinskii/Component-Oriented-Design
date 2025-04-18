using System;
using UnityEngine;

public sealed class JumpComponent
{
    public float JumpCooldown { get; private set; }

    public event Action OnJumped;
            
    private readonly float _jumpForce;
        
    public JumpComponent(float jumpForce, float coolDown)
    {
        _jumpForce = jumpForce;
        JumpCooldown = coolDown;        
    }

    private readonly Condition _condition = new();
        
    public void Jump(Vector2 direction, Rigidbody2D rb)
    {
        if (_condition.IsTrue())
        {
            rb.AddForce(new Vector2(0, direction.y * _jumpForce), ForceMode2D.Impulse);            
            OnJumped?.Invoke();           
        }        
    }

    public void AddCondition(Func<bool> condition)
    {
        _condition.AddCondition(condition);
    }     
}
