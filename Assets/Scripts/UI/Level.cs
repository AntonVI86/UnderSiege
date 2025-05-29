using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private DefeatPanel _defeatPanel;
    [SerializeField] private WinPanel _winPanel;
    [SerializeField] private Health _outpost;
    [SerializeField] private MonstersCountView _monstersCount;

    private void OnEnable()
    {
        _outpost.Defeated += OnDefeated;
        _monstersCount.Win += OnWin;
    }

    private void OnWin()
    {
        Time.timeScale = 0;
        _winPanel.Show();
    }

    private void OnDefeated()
    {
        Time.timeScale = 0;
        _defeatPanel.Show();
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
        _outpost.Defeated -= OnDefeated;
        _monstersCount.Win -= OnWin;
    }

}
