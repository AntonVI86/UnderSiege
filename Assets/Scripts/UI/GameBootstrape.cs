using UnityEngine;

public class GameBootstrape : MonoBehaviour
{
    [SerializeField] private EnemySpawner _spawner;
    [SerializeField] private BlockGenerator _blocks;
    [SerializeField] private SoundControl[] _soundControl;

    private void Start()
    {
        _blocks.GenerateGrid();
        _spawner.Launch();

        for (int i = 0; i < _soundControl.Length; i++)
        {
            _soundControl[i].SetVolume();
        }
    }
}
