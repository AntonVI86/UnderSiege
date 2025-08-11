using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundControl : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private AudioSource _audio;

    [SerializeField] private string _soundTypeName;

    public void SetVolume()
    {
        _slider.onValueChanged.AddListener(OnVolumeChange);

        if (PlayerPrefs.HasKey(_soundTypeName))
        {
            _audio.volume = PlayerPrefs.GetFloat(_soundTypeName);

        }

        _slider.value = _audio.volume;
    }

    private void OnVolumeChange(float value)
    {
        _audio.volume = value;
        PlayerPrefs.SetFloat(_soundTypeName, _audio.volume);
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(OnVolumeChange);
    }
}
