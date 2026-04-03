using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public abstract class BasePickUp : MonoBehaviour, IPickable
{
    public PickUpItem PickUpItem;
    [SerializeField] private PickUpStats _pickUpStats;
    protected Rigidbody2D _rigidbody2D;
    protected SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        _rigidbody2D.mass = _pickUpStats.Mass;
        _rigidbody2D.linearDamping = _pickUpStats.LinearDamping;
        _rigidbody2D.angularDamping = _pickUpStats.AngularDamping;
        _rigidbody2D.gravityScale = _pickUpStats.GravityScale;
    }
    private void Update()
    {
        if(transform.position.y < -10f)
        {
            Destroy(gameObject);
        }
    }
    public virtual GameObject OnPickUp()
    {
        _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, 1f);
        transform.DOPause();
        return gameObject;
    }
    public virtual void OnDrop()
    {
    }
    public virtual void Follow(Vector3 targetPositon)
    {
        Vector3 currentDirection = (targetPositon - transform.position).normalized;
        _rigidbody2D.linearVelocity = _pickUpStats.Speed * (targetPositon - transform.position).magnitude * currentDirection;
        float rotation = Mathf.DeltaAngle(_rigidbody2D.rotation, Mathf.Atan2(currentDirection.y, currentDirection.x) * Mathf.Rad2Deg);
        _rigidbody2D.angularVelocity = rotation * _pickUpStats.RotationSpeed * (targetPositon - transform.position).magnitude;
    }
    public virtual void OnCursorEnter()
    {
        _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, 0.5f);
    }
    public virtual void OnCursorExit()
    {
        _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, 1f);
    } 
}

public enum PickUpItem
{
    Rose,
    Scissors,
    TrashCan,
    Beard
}