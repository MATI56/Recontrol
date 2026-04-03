using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource audioSource;

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
    public void PlaySound(AudioClip audioClip)
    {
        audioSource.pitch = Random.Range(0.85f, 1.0f);
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
