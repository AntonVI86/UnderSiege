using UnityEngine;
using UnityEngine.UI;

public class OutpostHealthView : MonoBehaviour
{
    [SerializeField] private Image[] _hearts;
    [SerializeField] private Health _health;

    private void OnEnable()
    {
        _health.HealthValueChanged += InfoUpdate;
    }

    private void Start()
    {
        InfoUpdate(_health.CurrentHealth);
    }

    public void InfoUpdate(int value)
    {
        foreach (var heart in _hearts)
        {
            heart.gameObject.SetActive(false);
        }

        for (int i = 0; i < value; i++)
        {
            _hearts[i].gameObject.SetActive(true);
        }
    }

    private void OnDisable()
    {
        _health.HealthValueChanged -= InfoUpdate;
    }
}
