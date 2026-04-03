using DG.Tweening;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private int _pointsToWin = 10;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _startPhone;
    private int _currentPoints = 0;

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
    public void AddPoint()
    {
        _currentPoints++;
        if (_currentPoints >= _pointsToWin)
            _animator.SetTrigger("Win");
    }
    public void StartGame()
    {
        _currentPoints = 0;
        ObstacleManager.Instance.StartSpawning();
        PickUpManager.Instance.StartSpawning();
        _startPhone.transform.DOMoveY(-10, 1);
    }
    public void StopGame()
    {
        ObstacleManager.Instance.StopSpawning();
        PickUpManager.Instance.StopSpawning();
    }
}
