using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Wave : MonoBehaviour
{
    [SerializeField] private int _defaultEnemyCount = 10;

    [SerializeField] private TMP_Text _levelVisual;

    private int _enemyCount;
    private int _level = 1;

    public int Level => _level;
    public int EnemyCount => _enemyCount;

    private void Awake()
    {
        Load();
        _levelVisual.text = "Волна " + _level.ToString();
    }

    public void NextLevel()
    {
        _level++;
        _enemyCount = _defaultEnemyCount * _level + 5;

        PlayerPrefs.SetInt("Level", _level);
        PlayerPrefs.SetInt("EnemyCount", _enemyCount);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Load()
    {
        if (PlayerPrefs.HasKey("Level") == false)
        {
            _enemyCount = _defaultEnemyCount;
            return;
        }

        _level = PlayerPrefs.GetInt("Level");
        _enemyCount = PlayerPrefs.GetInt("EnemyCount");
    }
}
