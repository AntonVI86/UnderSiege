using UnityEngine;

public class GameBootstrape : MonoBehaviour
{
    [SerializeField] private EnemySpawner _spawner;
    [SerializeField] private BlockGenerator _blocks;

    private void Start()
    {
        _blocks.GenerateGrid();
        _spawner.Launch();
    }
}
