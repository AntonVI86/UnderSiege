using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private TMP_Text _cost;
    [SerializeField] private Button _button;

    [SerializeField] private AudioClip _purchaseSfx;
    [SerializeField] private AudioClip _errorSfx;

    private BoosterSO _booster;
    private Inventory _inventory;

    private void Awake()
    {
        _inventory = GetComponentInParent<Shop>().BoostersContainer;
    }

    public void Display(BoosterSO booster)
    {
        _booster = booster;
        _button.onClick.AddListener(() => Purchase(_booster));
        _icon.sprite = _booster.Icon;
        _title.text = Lean.Localization.LeanLocalization.GetTranslationText(_booster.Label);
        _description.text = Lean.Localization.LeanLocalization.GetTranslationText(_booster.Description);
        _cost.text = _booster.Cost.ToString();
    }

    private void Purchase(BoosterSO booster)
    {
        if(MoneyCounter.Instance.CoinAmount < booster.Cost)
        {
            SoundPlayer.Instance.PlaySound(_errorSfx);
            return;
        }

        SoundPlayer.Instance.PlaySound(_purchaseSfx);
        _inventory.AddBooster(booster);
        MoneyCounter.Instance.AddCoin(-booster.Cost);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(() => Purchase(_booster));
    }
}
