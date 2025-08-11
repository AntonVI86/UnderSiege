using UnityEngine;
using UnityEngine.UI;

public class WindowOpener : MonoBehaviour
{
    [SerializeField] private GameObject _window;
    [SerializeField] private AudioClip _openSfx;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnOpened);
    }

    private void OnOpened()
    {
        SoundPlayer.Instance.PlaySound(_openSfx);
        Time.timeScale = 0;
        _window.SetActive(true);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnOpened);
    }
}
