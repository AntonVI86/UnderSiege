using TMPro;
using UnityEngine;

public class MoneyView : MonoBehaviour
{
    [SerializeField] private TMP_Text _money;
    [SerializeField] private MoneyCounter _counter;

    private void OnEnable()
    {
        _counter.CoinValueChanged += OnCoinValueChanged;
    }

    private void OnDisable()
    {
        _counter.CoinValueChanged -= OnCoinValueChanged;
    }

    private void OnCoinValueChanged()
    {
        _money.text = _counter.CoinAmount.ToString();
    }
}
