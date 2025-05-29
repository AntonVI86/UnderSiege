using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShellView : MonoBehaviour
{
    [SerializeField] private ShellReceiver _receiver;
    [SerializeField] private TMP_Text _amount;
    [SerializeField] private Image _circle;

    [SerializeField] private TurretAttacker _attacker;

    private void OnEnable()
    {
        _receiver.Received += OnReceived;
        _attacker.ShellAmountChanged += OnShellAmountChanged;
    }

    private void OnDisable()
    {
        _receiver.Received -= OnReceived;
        _attacker.ShellAmountChanged -= OnShellAmountChanged;
    }

    private void OnReceived(Color color, int value)
    {
        _circle.gameObject.SetActive(true);
        _circle.color = color;
        _amount.text = value.ToString();
    }

    private void OnShellAmountChanged(int value)
    {
        _amount.text = value.ToString();

        if(value <= 0)
        {
            _circle.gameObject.SetActive(false);
        }
    }
}
