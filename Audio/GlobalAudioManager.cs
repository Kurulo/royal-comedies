using UnityEngine;


public class GlobalAudioManager : MonoBehaviour
{
    public static GlobalAudioManager Instance;

    [Header("Music")]
    [SerializeField] private AudioSource _musicAudionSource;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip _mainTmene;
    [SerializeField] private AudioClip _winerTheme;
    [SerializeField] private AudioClip _secondMainTheme;

    private float _currentVolumeValue = 1.5f;
    public float CurrentVolumeValue { get { return _currentVolumeValue; } }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            AudioListener.volume = _currentVolumeValue;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        OnPlayMainTheme();
    }

    public void ChangeVolume(float value)
    {
        _currentVolumeValue = value;
        AudioListener.volume = _currentVolumeValue;
    }

    public void OnPlayMainTheme()
    {
        PlayMusic(_mainTmene);
    }

    public void OnPlayWinerTheme()
    {
        PlayMusic(_winerTheme);
    }

    public void OnPlaySecondMainTheme()
    {
        PlayMusic(_secondMainTheme);
    }

    private void PlayMusic(AudioClip clip)
    {
        _musicAudionSource.Stop();
        _musicAudionSource.clip = clip;
        _musicAudionSource.Play();
    }
}
