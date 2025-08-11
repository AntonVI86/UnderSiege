using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameReseter : MonoBehaviour
{
    [SerializeField] private Button _button;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClickButton);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClickButton);
    }

    private void OnClickButton()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
    }
}
