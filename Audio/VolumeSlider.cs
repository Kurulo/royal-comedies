using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider _volumeSlider;
    void Start()
    {
        if (GlobalAudioManager.Instance != null)
        {
            _volumeSlider.value = GlobalAudioManager.Instance.CurrentVolumeValue;
            _volumeSlider.onValueChanged.AddListener(val => GlobalAudioManager.Instance.ChangeVolume(val));
        }
    }
}
