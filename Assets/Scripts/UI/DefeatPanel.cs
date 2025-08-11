using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DefeatPanel : Panel
{
    [SerializeField] private Button _next;
    [SerializeField] private Button _showReward;

    [SerializeField] private AudioClip _openSfx;

    private void OnEnable()
    {
        _next.onClick.AddListener(OnClickNextButton);
        _showReward.onClick.AddListener(OnClickShowRewardButton);
        SoundPlayer.Instance.StopPlayingMusic();
        SoundPlayer.Instance.PlaySound(_openSfx);
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
