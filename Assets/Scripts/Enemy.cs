using UnityEngine;
using UniRx;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, IMovable, IDamageable
{
    [SerializeField] private float _speed;

    private CompositeDisposable _moveDisposible = new CompositeDisposable();

    public event UnityAction<Enemy> Died;


    private void Start()
    {
        _speed = Random.Range(0.5f, 1.5f);
        Move();
    }

    public void Move()
    {      
        Observable.EveryUpdate().Subscribe(_=> 
        {
            Ray ray = new Ray(transform.position, transform.forward);

            RaycastHit hit;

            transform.Translate(Vector3.forward * _speed * Time.deltaTime); 

            if(Physics.Raycast(ray, out hit, 0.3f))
            {
                if(hit.transform.TryGetComponent(out Wall wall))
                {
                    EnemyDestroy();
                }
            }
        }).AddTo(_moveDisposible);
    }

    public void EnemyDestroy()
    {
        Died?.Invoke(this);
        _moveDisposible.Clear();
        Destroy(gameObject);
    }

    private void OnDisable()
    {
        _moveDisposible.Clear();
    }

    public void ApplyDamage(float damage)
    {
        throw new System.NotImplementedException();
    }
}
