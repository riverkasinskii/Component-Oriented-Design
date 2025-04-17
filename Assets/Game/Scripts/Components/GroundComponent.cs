public sealed class GroundComponent
{    
    private bool _isGrounded;    
    
    public bool GetGroundState()
    {
        return _isGrounded;
    }

    public void SetGroundState(bool state)
    {
        _isGrounded = state;
    }
}
