using UnityEngine;

public class ScissorsPickUp : BasePickUp
{
    public override GameObject OnPickUp()
    {
        return base.OnPickUp();
    }
    public override void OnDrop()
    {
        base.OnDrop();
    }
    public override void Follow(Vector3 targetPosition)
    {
        base.Follow(targetPosition);
    }
    public override void OnCursorEnter()
    {
        base.OnCursorEnter();
    }
    public override void OnCursorExit()
    {
        base.OnCursorExit();
    }
}
