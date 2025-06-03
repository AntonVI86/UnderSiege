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
    [SerializeField] private Animator _animator;
    [SerializeField] private Image _goldSlot;
    [SerializeField] private Wave _wave;

    [SerializeField] private TMP_Text _currentMoney;

    private int _tempCoinsCount;

    private void OnEnable()
    {
        _rewardButton.onClick.AddListener(OnClickShowRewardButton);
        _nextButton.onClick.AddListener(OnClickNextButton);
        _getButton.onClick.AddListener(OnClickGetButton);
    }

    public void OnClickGetButton()
    {
        _getButton.enabled = false;
        _rewardButton.gameObject.SetActive(true);
        _tempCoinsCount = Random.Range(10, 50);
        _animator.CrossFade("Chest_Opened", 0.1f);
        _goldSlot.gameObject.SetActive(true);
        _rewardButton.gameObject.SetActive(true);
        _currentMoney.text = _tempCoinsCount.ToString();
    }

    public override void OnClickNextButton()
    {
        MoneyCounter.Instance.AddCoin(_tempCoinsCount);
        PlayerPrefs.SetInt("Money", MoneyCounter.Instance.CoinAmount);

        _wave.NextLevel();
    }

    public override void OnClickShowRewardButton()
    {
        _tempCoinsCount *= 2;
        _currentMoney.text = _tempCoinsCount.ToString();
        _rewardButton.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        _rewardButton.onClick.RemoveListener(OnClickShowRewardButton);
        _nextButton.onClick.RemoveListener(OnClickNextButton);
        _getButton.onClick.RemoveListener(OnClickGetButton);
    }
}
