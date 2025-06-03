using UnityEngine.UI;

public class HealBooster : Booster
{
    private Button _button;
    private Slot _slot;

    private Health _outpostHealth;

    private void Awake()
    {
        _slot = GetComponent<Slot>();
        _button = GetComponent<Button>();
        _outpostHealth = GetComponentInParent<HealthGetter>().OutpostHealth;
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(Use);
    }

    public override void Use()
    {
        if (_outpostHealth.CurrentHealth == _outpostHealth.MaxHealth)
        {
            print("������ �������������");
            return;
        }

        _outpostHealth.Heal();
        _slot.ClearSlot();
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(Use);
    }
}
