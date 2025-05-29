using UnityEngine;
using UnityEngine.UI;

public class BoosterBonusSlot : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Image _wing;

    private BoosterSO _currentBooster;
    private Button _button;
    private DefeatBoosterBonus _panel;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _panel = GetComponentInParent<DefeatBoosterBonus>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(SetBonus);
    }

    public void Display(BoosterSO booster)
    {
        _icon.sprite = booster.Icon;
        _currentBooster = booster;
    }

    public void RemoveWing()
    {
        _wing.gameObject.SetActive(false);
    }

    private void SetBonus()
    {
        _panel.Opt(_currentBooster);
        _wing.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(SetBonus);
    }
}
