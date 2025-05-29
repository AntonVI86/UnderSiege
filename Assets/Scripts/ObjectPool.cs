using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private int _capacity;

    private List<Bullet> _pool = new List<Bullet>();

    //protected void Initialize(GameObject[] prefab)
    //{
    //    for (int i = 0; i < _capacity; i++)
    //    {
    //        int index = Random.Range(0, prefab.Length);
    //        Bullet spawned = Instantiate(prefab[index], _container.transform);

    //        spawned.gameObject.SetActive(false);

    //        _pool.Add(spawned);
    //    }
    //}

    protected void Initialize(Bullet prefab)
    {
        for (int i = 0; i < _capacity; i++)
        {
            Bullet spawned = Instantiate(prefab);
            spawned.transform.SetParent(_container);
            spawned.gameObject.SetActive(false);

            _pool.Add(spawned);
        }
    }

    protected bool TryGetObject(out Bullet result)
    {
        result = _pool.FirstOrDefault(p => p.gameObject.activeSelf == false);

        return result != null;
    }

    public void ResetPool()
    {
        foreach (var item in _pool)
        {
            item.gameObject.SetActive(false);
        }
    }
}
