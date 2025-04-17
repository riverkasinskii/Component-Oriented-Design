using System;
using UnityEngine;

public interface IMoveInput
{
    event Action<Vector2> OnInputInvoked;
}
