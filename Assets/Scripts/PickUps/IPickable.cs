using Unity.VisualScripting;
using UnityEngine;

public interface IPickable
{
    public GameObject OnPickUp();
    public void OnDrop();
    public void Follow(Vector3 targetPosition);
    public void OnCursorEnter();
    public void OnCursorExit();
}
