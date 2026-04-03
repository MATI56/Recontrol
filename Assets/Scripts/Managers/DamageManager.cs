using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DamageManager : MonoBehaviour
{
    public static DamageManager Instance { get; private set; }
    [SerializeField] private Animator _animator;
    [SerializeField] private Light2D _light2D;
    [SerializeField] private AudioClip[] _damageSounds;

    [SerializeField] private int _startHealth = 5;
    private int _healthPoints;
    
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _healthPoints = _startHealth;
    }
    private void Update()
    {
        if(_light2D.color != Color.white)
            _light2D.color = Color.Lerp(_light2D.color, Color.white, Time.deltaTime * 5f);
    }
    public void TakeDamage()
    {
        _healthPoints--;
        _light2D.color = Color.red;
        _animator.SetTrigger("Damage");
        AudioManager.Instance.PlaySound(_damageSounds[Random.Range(0, _damageSounds.Length)]);
        if(_healthPoints <= 0)
        {
            _animator.SetTrigger("Dead");
            _healthPoints = _startHealth;
        }
    }
}
