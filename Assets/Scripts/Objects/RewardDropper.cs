using UnityEngine;

public class RewardDropper : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;

    private int _minValue = 1;
    private int _maxValue = 10;
    private int _minChanceValue = 8;

    private float _spawnPositionY = 2.85f;

    public void Drop(Enemy enemy)
    {
        int currentValue = Random.Range(_minValue, _maxValue);

        if(currentValue > _minChanceValue)
        {
            Coin coin = Instantiate(_coinPrefab);
            coin.transform.SetParent(null);

            Vector3 newPosition = new Vector3(enemy.transform.position.x, _spawnPositionY, enemy.transform.position.z);
            coin.transform.position = newPosition;

            coin.PlayDropOutSound();
        }
    }
}
