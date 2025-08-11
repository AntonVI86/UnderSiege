using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private ShopItem _itemTemplate;
    [SerializeField] private List<BoosterSO> _items;

    [SerializeField] private Inventory _inventory;
    public Inventory BoostersContainer => _inventory;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        foreach (BoosterSO item in _items)
        {
            ShopItem newItem = Instantiate(_itemTemplate, transform);
            newItem.Display(item);
        }
    }
}
