using UnityEngine;

[CreateAssetMenu(fileName = "PickUpStats", menuName = "PickUp/PickUpStats")]
public class PickUpStats : ScriptableObject
{
    public float Speed = 10f;
    public float RotationSpeed = 10f;
    public float Mass = 1f;
    public float LinearDamping = 0.5f;
    public float AngularDamping = 0.5f;
    public float GravityScale = 1f;
}
