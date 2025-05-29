using System.Collections.Generic;
using UnityEngine;

public class Inventory: MonoBehaviour
{
    [SerializeField] private List<Slot> _slots = new List<Slot>();

    private void Awake()
    {
        Load();
    }

    public void AddBooster(BoosterSO booster)
    {
        foreach (Slot slot in _slots)
        {
            if(slot.Booster == booster)
            {
                slot.Display(booster);
                break;
            }
        }

        Save();
    }

    public void Save()
    {
        for (int i = 0; i < _slots.Count; i++)
        {
            PlayerPrefs.SetInt("Slot" + i, _slots[i].Count);
        }
    }

    public void Load()
    {
        for (int i = 0; i < _slots.Count; i++)
        {
            if(PlayerPrefs.HasKey("Slot" + i))
            {
                _slots[i].Load("Slot" + i);
            }
        }

    }
}
