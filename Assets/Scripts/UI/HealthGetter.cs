using UnityEngine;

public class HealthGetter : MonoBehaviour
{
    [SerializeField] private Health _outpostHealth;

    public Health OutpostHealth => _outpostHealth;
}
