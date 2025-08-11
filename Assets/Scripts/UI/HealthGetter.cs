using UnityEngine;

public class HealthGetter : MonoBehaviour
{
    [SerializeField] private Health _outpostHealth;
    [SerializeField] private EnemySpawner _spawner;
    [SerializeField] private Timer _timer;

    public Health OutpostHealth => _outpostHealth;
    public EnemySpawner Spawner => _spawner;
    public Timer FreezeTimer => _timer;
}
