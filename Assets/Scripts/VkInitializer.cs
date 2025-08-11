using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VkInitializer : MonoBehaviour
{
    [SerializeField] private GameObject _alertWindow;
    [SerializeField] private Button _okButton;

    private void OnEnable()
    {
        _okButton.onClick.AddListener(OnClickButton); 
    }
    private IEnumerator Start()
    {
#if UNITY_EDITOR
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        yield break;
#else
        yield return Agava.VKGames.VKGamesSdk.Initialize(onSuccessCallback: () => _alertWindow.SetActive(true));
#endif
    }
    private void OnClickButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OnDisable()
    {
        _okButton.onClick.RemoveListener(OnClickButton);
    }
}
