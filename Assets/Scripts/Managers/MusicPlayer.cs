using System.Collections;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private AudioSource _audioSource;
    private float _volume = 0.1f;
    [SerializeField] private AudioClip[] _musics;


    public static MusicPlayer Instance { get; private set; }

    private void Awake()
    {
        #region SingletonPattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        #endregion

        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = _volume;
        StartCoroutine(MusicLoop());
    }

    private int GetRandomIndex()
    {
        return Random.Range(0, _musics.Length);
    }

    private IEnumerator MusicLoop()
    {
        while (true)
        {
            AudioClip clip = _musics[GetRandomIndex()];
            _audioSource.clip = clip;
            _audioSource.Play();

            while (_audioSource.isPlaying)
            {
                yield return null;
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

}
