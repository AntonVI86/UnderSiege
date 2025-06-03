using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DefeatBoosterBonus : MonoBehaviour
{
    [SerializeField] private List<BoosterSO> _boosters = new List<BoosterSO>();
    [SerializeField] private List<BoosterBonusSlot> _slots = new List<BoosterBonusSlot>();
    [SerializeField] private TMP_Text _info;

    [SerializeField] private Inventory _inventory;
    [SerializeField] private Button _button;

    private BoosterSO _currentBooster;


    private void OnEnable()
    {
        _button.onClick.AddListener(AcceptChoice);
    }

    private void Start()
    {
        _button.enabled = false;

        ShuffleArray();

        for (int i = 0; i < _slots.Count; i++)
        {
            _slots[i].Display(_boosters[i]);
        }
    }

    public void Opt(BoosterSO booster)
    {
        foreach (var slot in _slots)
        {
            slot.RemoveWing();
        }

        _button.enabled = true;
        _currentBooster = booster;
        _info.text = Lean.Localization.LeanLocalization.GetTranslationText(booster.Description);
    }

    private void AcceptChoice()
    {
        _inventory.AddBooster(_currentBooster);
        _button.GetComponentInChildren<TMP_Text>().text = "Получено";
        _button.enabled = false;
    }

    public void ShuffleArray()
    {
        for (int i = _boosters.Count - 1; i >= 0; i--)
        {
            int randomPosition = Random.Range(0, _boosters.Count);

            BoosterSO booster = _boosters[randomPosition];
            _boosters[randomPosition] = _boosters[i];
            _boosters[i] = booster;
        }
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(AcceptChoice);
    }
}
