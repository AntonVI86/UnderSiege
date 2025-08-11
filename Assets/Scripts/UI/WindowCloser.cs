using UnityEngine;
using UnityEngine.UI;

public class WindowCloser : MonoBehaviour
{
    [SerializeField] private GameObject _window;
    [SerializeField] private AudioClip _closeSfx;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClosed);
    }

    private void OnClosed()
    {
        Time.timeScale = 1;
        _window.SetActive(false);
        SoundPlayer.Instance.PlaySound(_closeSfx);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClosed);
    }
}
