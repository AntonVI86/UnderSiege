using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip[] _musicTracks;

    [SerializeField] private AudioSource _musicSource;
    
    public static SoundPlayer Instance;

    private AudioSource _audio;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();

        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        PlayMusic();
    }

    public void PlayMusic()
    {
        int musicNumber = Random.Range(0, _musicTracks.Length);

        _musicSource.PlayOneShot(_musicTracks[musicNumber]);
    }

    public void StopPlayingMusic() => _musicSource.Stop();

    public void PlaySound(AudioClip clip)
    {
        _audio.PlayOneShot(clip);
    }
}
