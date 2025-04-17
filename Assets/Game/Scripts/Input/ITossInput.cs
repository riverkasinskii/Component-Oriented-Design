using System;
using UnityEngine;

public interface ITossInput
{
    event Action<Vector2> OnInputInvoked;
}
