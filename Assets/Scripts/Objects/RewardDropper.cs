using UnityEngine;

public class RewardDropper : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;

    private int _minValue = 1;
    private int _maxValue = 10;
    private int _minChanceValue = 8;

    public void Drop(Enemy enemy)
    {
        int currentValue = Random.Range(_minValue, _maxValue);

        if(currentValue > _minChanceValue)
        {
            Coin coin = Instantiate(_coinPrefab);
            coin.transform.SetParent(null);
            coin.transform.position = new Vector3(enemy.transform.position.x, 2.8f, enemy.transform.position.z);
        }
    }
}
