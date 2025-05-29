using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth;

    public event UnityAction Defeated;
    public event UnityAction<int> HealthValueChanged;

    private int _currentHealth;

    public int CurrentHealth => _currentHealth;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void Heal()
    {
        _currentHealth = _maxHealth;
        HealthValueChanged?.Invoke(_currentHealth);
    }

    public void ApplyDamage()
    {
        _currentHealth--;
        HealthValueChanged?.Invoke(_currentHealth);

        if(_currentHealth <= 0)
        {
            Defeated?.Invoke();
        }
    }
}
