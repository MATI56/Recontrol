using DG.Tweening;
using UnityEngine;

public abstract class BaseObstacle : MonoBehaviour, IObstacle
{
    [SerializeField] private PickUpItem _correctItem;
    [SerializeField] private MoveToTarget[] moveToTargets;
    [SerializeField] private AudioClip _correctSound;
    private MoveToTarget _currentMoveToTarget;

    private void Update()
    {
        if(transform.position == _currentMoveToTarget.TargetPosition)
        {
            DamageManager.Instance.TakeDamage();
            Despawn();
        }
    }
    public virtual void OnSpawn()
    {
        _currentMoveToTarget = moveToTargets[Random.Range(0, moveToTargets.Length)];
        if(_currentMoveToTarget.FlipX)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
        if(_currentMoveToTarget.FlipY)
        {
            Vector3 scale = transform.localScale;
            scale.y *= -1;
            transform.localScale = scale;
        }

        transform.position = _currentMoveToTarget.SpawnPosition;
        transform.rotation = _currentMoveToTarget.SpawnRotation;
        transform.DOMove(_currentMoveToTarget.TargetPosition, _currentMoveToTarget.MoveDuration);
        transform.DORotateQuaternion(_currentMoveToTarget.TargetRotation, _currentMoveToTarget.RotateDuration);
    }
    public virtual void Despawn()
    {
        transform.DOPause();
        Destroy(gameObject);
    }

    public virtual void OnWrongItemCollision()
    {
    }

    public virtual void OnCorrectItemCollision()
    {
        GameManager.Instance.AddPoint();
        AudioManager.Instance.PlaySound(_correctSound);
        Despawn();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
     if(collision.gameObject.TryGetComponent(out BasePickUp pickUp))
        {
            if(pickUp.PickUpItem == _correctItem)
            {
                OnCorrectItemCollision();
            }
            else
            {
                OnWrongItemCollision();
            }
        }
    }
    [System.Serializable]
    private class MoveToTarget
    {
        public Vector3 SpawnPosition;
        public Quaternion SpawnRotation;
        public Vector3 TargetPosition;
        public Quaternion TargetRotation;

        public float MoveDuration;
        public float RotateDuration;

        public bool FlipX;
        public bool FlipY;
    }
}
public interface IObstacle
{
    public void OnSpawn();
    public void Despawn();
    public void OnCorrectItemCollision();
    public void OnWrongItemCollision();  
}
