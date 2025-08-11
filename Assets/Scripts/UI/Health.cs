using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private AudioClip _damageSfx;
    [SerializeField] private Animator _damage;

    public event UnityAction Defeated;
    public event UnityAction<int> HealthValueChanged;

    private int _currentHealth;

    public int CurrentHealth => _currentHealth;
    public int MaxHealth => _maxHealth;

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
        _damage.Play("Damage");
        HealthValueChanged?.Invoke(_currentHealth);
        SoundPlayer.Instance.PlaySound(_damageSfx);

        if(_currentHealth <= 0)
        {
            Defeated?.Invoke();
        }
    }
}
