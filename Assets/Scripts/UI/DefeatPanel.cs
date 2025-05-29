using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DefeatPanel : Panel
{
    [SerializeField] private Button _next;
    [SerializeField] private Button _showReward;

    private void OnEnable()
    {
        _next.onClick.AddListener(OnClickNextButton);
        _showReward.onClick.AddListener(OnClickShowRewardButton);
    }

    private void OnDisable()
    {
        _next.onClick.RemoveListener(OnClickNextButton);
        _showReward.onClick.RemoveListener(OnClickShowRewardButton);
    }

    public override void OnClickNextButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public override void OnClickShowRewardButton()
    {
        //Показать рекламу

        //Добавить бутстрап
    }
}
