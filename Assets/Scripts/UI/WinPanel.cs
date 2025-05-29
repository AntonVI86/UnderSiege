using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinPanel : Panel
{
    [SerializeField] private Button _rewardButton;
    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _getButton;

    [SerializeField] private TMP_Text _currentMoney;

    private int _tempCoinsCount;

    private void OnEnable()
    {
        _rewardButton.onClick.AddListener(OnClickShowRewardButton);
        _nextButton.onClick.AddListener(OnClickNextButton);
        _getButton.onClick.AddListener(OnClickGetButton);
    }

    private void OnDisable()
    {
        _rewardButton.onClick.RemoveListener(OnClickShowRewardButton);
        _nextButton.onClick.RemoveListener(OnClickNextButton);
        _getButton.onClick.RemoveListener(OnClickGetButton);
    }

    private void Start()
    {
        
    }

    public void OnClickGetButton()
    {
        _getButton.gameObject.SetActive(false);
        _rewardButton.gameObject.SetActive(true);
        _tempCoinsCount = Random.Range(10, 50);
        _currentMoney.text = _tempCoinsCount.ToString() + " монет";
    }

    public override void OnClickNextButton()
    {
        MoneyCounter.Instance.AddCoin(_tempCoinsCount);
        PlayerPrefs.SetInt("Money", MoneyCounter.Instance.CoinAmount);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public override void OnClickShowRewardButton()
    {
        _tempCoinsCount *= 2;
        _currentMoney.text = _tempCoinsCount.ToString() + " монет";
        _rewardButton.gameObject.SetActive(false);
    }
}
