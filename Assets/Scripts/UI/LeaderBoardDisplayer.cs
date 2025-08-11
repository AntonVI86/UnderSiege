using UnityEngine;
using UnityEngine.UI;

public class LeaderBoardDisplayer : MonoBehaviour
{
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClickButton);
    }

    private void OnClickButton()
    {
        if(PlayerPrefs.HasKey("Level") == false)
        {
            Agava.VKGames.Leaderboard.ShowLeaderboard(0);
            return;
        }

        Agava.VKGames.Leaderboard.ShowLeaderboard(PlayerPrefs.GetInt("Level"));
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClickButton);
    }
}
