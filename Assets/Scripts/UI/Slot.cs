using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _countVisual;

    [SerializeField] private BoosterSO _booster;
    private int _count;

    public BoosterSO Booster => _booster;
    public int Count => _count;

    private void Start()
    {
        _countVisual.text = _count.ToString();

        if (_count <= 0)
        {
            HideSlot();
        }
    }

    public void Display(BoosterSO booster)
    {
        gameObject.SetActive(true);
        _count++;
        _countVisual.text = _count.ToString();
    }

    public void Cancel()
    {
        if(_count <= 0)
        {
            gameObject.SetActive(true);
            _count++;
            _countVisual.text = _count.ToString();
            return;
        }

        _count++;
        _countVisual.text = _count.ToString();
    }

    private void HideSlot() => gameObject.SetActive(false);

    public void ClearSlot()
    {
        _count--;
        _countVisual.text = _count.ToString();

        if (_count <= 0)
        {
            gameObject.SetActive(false);

            //InventoryPlayer.Instance.PlayerInventory.RemoveSlot(this);
        }
    }

    public void Load(string name)
    {
        _count = PlayerPrefs.GetInt(name);
    }
}
