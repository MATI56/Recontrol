using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControler : MonoBehaviour
{
    [SerializeField] private GameObject _Cursor;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite _CursorDefaultSprite;
    [SerializeField] private Sprite _CursorPickUpSprite;

    [SerializeField] private AudioClip _clickSound;

    private GameObject _CurrentHoldObject;
    private IPickable _CurrentPickableObject;

    private void Start()
    {
        InputManager.Instance.InputActions.Gameplay.Interact.performed += UpdateInteract;
        InputManager.Instance.InputActions.Gameplay.Interact.canceled += UpdateInteract;
    }
    private void FixedUpdate()
    {
        UpdateCursor();
        if (_CurrentHoldObject != null)
            UpdateHoldObject();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IPickable>(out IPickable pickable))
        {
            if(_CurrentPickableObject != null)
                _CurrentPickableObject.OnCursorExit();
            _CurrentPickableObject = pickable;
            _CurrentPickableObject.OnCursorEnter();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IPickable>(out IPickable pickable) && pickable == _CurrentPickableObject)
        {
            _CurrentPickableObject.OnCursorExit();
            _CurrentPickableObject = null;
        }
    }
    private void UpdateInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            spriteRenderer.sprite = _CursorPickUpSprite;
            if (_CurrentPickableObject != null)
            {
                PickUpItem();
                AudioManager.Instance.PlaySound(_clickSound);
            }
        }
        else if (context.canceled)
        {
            spriteRenderer.sprite = _CursorDefaultSprite;
            if (_CurrentHoldObject != null)
                DropItem();
        } 
    }
    private void PickUpItem()
    {
        _CurrentHoldObject = _CurrentPickableObject.OnPickUp();
    }
    private void DropItem()
    {
        _CurrentHoldObject.GetComponent<IPickable>().OnDrop();
        _CurrentHoldObject = null;
    }
    private void UpdateCursor()
    {
        Vector2 inputPossition = InputManager.Instance.InputActions.Gameplay.CursorPosition.ReadValue<Vector2>();
        Vector3 targetPossition = new Vector3(inputPossition.x, inputPossition.y, 0);
        _Cursor.GetComponent<Rigidbody2D>().MovePosition(Camera.main.ScreenToWorldPoint(targetPossition));
    }
    private void UpdateHoldObject()
    {
        _CurrentHoldObject.GetComponent<IPickable>().Follow(_Cursor.transform.position);
    }
}