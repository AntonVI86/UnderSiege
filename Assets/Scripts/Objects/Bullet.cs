using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    
    private float _speed = 105f;

    private CompositeDisposable _disposible = new CompositeDisposable();

    private float _lifeTime = 0.8f;

    private void OnEnable()
    {
        _lifeTime = 0.8f;
    }
    public void MoveToTarget(Enemy enemy)
    {
        Observable.EveryUpdate().Subscribe(_=> 
        {
            transform.Translate(Vector3.forward * -_speed * Time.deltaTime);

            _lifeTime -= Time.deltaTime;

            if (_lifeTime <= 0)
                gameObject.SetActive(false);
            
        }).AddTo(_disposible);
    }

    public void ChangeColor(int layer,Material material)
    {
        _renderer.material = material;
        gameObject.layer = layer;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Enemy zombie))
        {
            zombie.EnemyDestroy();
            _disposible.Clear();
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        _disposible.Clear();
    }
}
