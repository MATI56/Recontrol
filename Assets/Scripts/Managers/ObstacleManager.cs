using DG.Tweening;
using NUnit.Framework;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public static ObstacleManager Instance { get; private set; }

    [SerializeField] private GameObject[] _obstacles;
    [SerializeField] private float _spawnInterval = 2f;

    private bool _isActive = false;
    private float _timer;

    private void Awake()
    {
        if(Instance == null)
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
        if(_isActive == false) return;

        _timer += Time.deltaTime;
        if(_timer >= _spawnInterval)
        {
            GameObject Temp = Instantiate(_obstacles[Random.Range(0, _obstacles.Length)], transform);
            Temp.GetComponent<BaseObstacle>().OnSpawn();
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
}
