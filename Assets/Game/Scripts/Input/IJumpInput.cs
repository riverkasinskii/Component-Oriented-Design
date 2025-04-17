using System;
using UnityEngine;

public interface IJumpInput
{
    event Action<Vector2> OnInputInvoked;    
}
