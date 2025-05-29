using UnityEngine;
using UnityEngine.Events;

public class MoneyCounter : MonoBehaviour
{
    public event UnityAction CoinValueChanged;

    public static MoneyCounter Instance;

    private int _coinAmount;

    public int CoinAmount => _coinAmount;

    private void Awake()
    {
        Instance = null;

        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("Money"))
            _coinAmount = PlayerPrefs.GetInt("Money");

        CoinValueChanged?.Invoke();
    }

    public void AddCoin(int value)
    {
        _coinAmount += value;
        PlayerPrefs.SetInt("Money", _coinAmount);
        CoinValueChanged?.Invoke();
    }
}
