using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Enemy> _enemyPrefabs;
    [SerializeField] private MonstersCountView _countView;
    [SerializeField] private int _enemyAmount = 50;
    [SerializeField] private Wave _wave;

    public List<Enemy> EnemiesOnField = new List<Enemy>();

    private float _minXPosition = -6.5f;
    private float _maxXPosition = 9f;
    private float _zStartPosition = -11.9f;
    private float _yDefaultPosition = 2.53f;

    public int EnemyAmount => _enemyAmount;

    public void Launch()
    {
        _enemyAmount = _wave.EnemyCount;

        StartCoroutine(Create());
    }

    private IEnumerator Create()
    {
        var delay = new WaitForSeconds(1f);

        for (int i = 0; i < _enemyAmount; i++)
        {
            int prefabNumber = Random.Range(0, _enemyPrefabs.Count);
            Vector3 newPosition = new Vector3(Random.Range(_minXPosition, _maxXPosition), _yDefaultPosition, _zStartPosition);
            Enemy newEnemy = Instantiate(_enemyPrefabs[prefabNumber], transform);
            newEnemy.transform.position = newPosition;

            EnemiesOnField.Add(newEnemy);

            newEnemy.Died += OnDied;

            yield return delay;
        }
    }

    private void OnDied(Enemy enemy)
    {
        _countView.Show();
        EnemiesOnField.Remove(enemy);
        enemy.Died -= OnDied;
        enemy.EnemyDestroy();
    }
}
