using DG.Tweening;
using UnityEngine;

public class PickUpManager : MonoBehaviour
{
    public static PickUpManager Instance { get; private set; }

    [SerializeField] private GameObject[] _pickUpPrefabs;
    [SerializeField] private Vector3 _spawnPosition;
    [SerializeField] private Vector3 _TargetPosition;
    [SerializeField] private float _moveDuration = 1f;
    [SerializeField] private float _spawnInterval = 2f;

    private int _currentPickUpIndex = 0;

    private bool _isActive = false;
    private float _timer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        if (_isActive == false) return;

        _timer += Time.deltaTime;
        if(_timer >= _spawnInterval)
        {
            SpawnPickUp();
            _timer = 0;
        }
    }
    public void StartSpawning()
    {
        _isActive = true;
    }
    public void StopSpawning()
    {
        _isActive = false;
        foreach(Transform child in transform)
        {
            child.transform.DOPause();
            Destroy(child.gameObject);
        }
    }
    private void SpawnPickUp()
    {
        GameObject pickUpPrefab = _pickUpPrefabs[_currentPickUpIndex];
        _currentPickUpIndex = (_currentPickUpIndex + 1) % _pickUpPrefabs.Length;
        GameObject pickUp = Instantiate(pickUpPrefab, _spawnPosition, Quaternion.identity, transform);
        pickUp.transform.DOMove(_TargetPosition, _moveDuration).SetEase(Ease.Linear);
    }
}
