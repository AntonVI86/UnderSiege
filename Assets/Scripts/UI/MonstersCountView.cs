using System;
using TMPro;
using UnityEngine;

public class MonstersCountView : MonoBehaviour
{
    [SerializeField] private TMP_Text _monsters;
    [SerializeField] private EnemySpawner _spawner;

    public event Action Win;

    private int _count;

    private void Start()
    {
        _count = _spawner.EnemyAmount;
        _monsters.text = _count.ToString();
    }

    public void Show()
    {
        _count--;
        _monsters.text = _count.ToString();

        if (_count <= 0)
            Win?.Invoke();
    }
}
