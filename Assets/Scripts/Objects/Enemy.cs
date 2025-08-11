using UnityEngine;
using UniRx;
using UnityEngine.Events;
using System.Collections;

public class Enemy : MonoBehaviour, IMovable, IDamageable
{
    [SerializeField] private ParticleSystem _deathVfx;
    [SerializeField] private AudioClip _deathSfx;

    private float _defaultSpeed;
    private float _currentSpeed;

    private CompositeDisposable _moveDisposible = new CompositeDisposable();

    public event UnityAction<Enemy> Died;
    private RewardDropper _dropper;

    private Animator _animator;


    private void Awake()
    {
        _dropper = GetComponent<RewardDropper>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _defaultSpeed = Random.Range(0.6f, 1.2f);
        _currentSpeed = _defaultSpeed;
        Move();
    }

    public void Move()
    {      
        Observable.EveryUpdate().Subscribe(_=> 
        {
            Ray ray = new Ray(transform.position, transform.forward);

            RaycastHit hit;

            transform.Translate(Vector3.forward * _currentSpeed * Time.deltaTime); 

            if(Physics.Raycast(ray, out hit, 0.3f))
            {
                if(hit.transform.TryGetComponent(out Health health))
                {
                    health.ApplyDamage();
                    EnemyDestroy();
                }
            }
        }).AddTo(_moveDisposible);
    }

    public void EnemyDestroy()
    {
        _dropper.Drop(this);
        Died?.Invoke(this);
        _moveDisposible.Clear();
        ParticleSystem vfx = Instantiate(_deathVfx);
        vfx.transform.SetParent(null);
        vfx.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        SoundPlayer.Instance.PlaySound(_deathSfx);
        Destroy(gameObject);
    }

    public void ChangeSpeed(float time)
    {
        StartCoroutine(Freeze(time));
    }

    private IEnumerator Freeze(float time)
    {
        _currentSpeed = 0;
        _animator.CrossFade("Idle", 0.1f);
        yield return new WaitForSeconds(time);
        _currentSpeed = _defaultSpeed;
        _animator.CrossFade("Walk", 0.1f);
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
