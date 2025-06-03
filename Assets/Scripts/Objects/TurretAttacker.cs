using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using UnityEngine.Events;

public class TurretAttacker : ObjectPool
{
    [SerializeField] private EnemySpawner _spawner;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private float _delayToAttack;
    [SerializeField] private ShellReceiver _receiver;

    public event UnityAction<int> ShellAmountChanged;
    private IntReactiveProperty _shellsAmount = new IntReactiveProperty();

    private CompositeDisposable _disposible = new CompositeDisposable();
    private CompositeDisposable _attackDisposible = new CompositeDisposable();
    private CompositeDisposable _rotateDisposible = new CompositeDisposable();

    private Enemy _currentTarget;

    private Coroutine _coroutine;
    private Vector3 _defaultRotation;

    private void Start()
    {
        Initialize(_bulletPrefab);

        _defaultRotation = transform.rotation.eulerAngles;

        _shellsAmount.Subscribe(_ => 
        {
            if(_shellsAmount.Value <= 0)
            {
                _receiver.ResetTurret();
                _attackDisposible.Clear();
                ShellAmountChanged?.Invoke(_shellsAmount.Value);
            }
        }).AddTo(_disposible);
    }

    public void GetShells(int value)
    {
        _shellsAmount.Value += value;
        ShellAmountChanged?.Invoke(_shellsAmount.Value);

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        StartCoroutine(Attack());
    }

    private bool CompareFirstCharacters(string first, string second)
    {
        if (first.Substring(0, 3) == second.Substring(0, 3))
        {
            return true;
        }

        return false;
    }

    private void FindEnemy(List<Enemy> enemies)
    {
        List<Enemy> applicableEnemies = new List<Enemy>();

        foreach (var enemy in enemies)
        {
            if(CompareFirstCharacters(enemy.gameObject.name, _receiver.Render.material.name))
            {
                applicableEnemies.Add(enemy);
            }
        }
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        if(applicableEnemies.Count > 0)
        {
            foreach (var applicableEnemy in applicableEnemies)
            {
                Vector3 diff = applicableEnemy.transform.position - position;
                float currentDistance = diff.sqrMagnitude;

                if(currentDistance < distance)
                {
                    _currentTarget = applicableEnemy;
                    distance = currentDistance;
                }
            }

        }
    }

    private IEnumerator Attack()
    {
        FindEnemy(_spawner.EnemiesOnField);

        while(_shellsAmount.Value > 0)
        {           
            if (_currentTarget != null)
            {
                RotateToTarget();

                yield return new WaitForSeconds(0.8f);
                
                if(TryGetObject(out Bullet newBullet))
                {
                    SetObject(newBullet, transform.position);
                    
                    _shellsAmount.Value--;
                    ShellAmountChanged?.Invoke(_shellsAmount.Value);
                }

            }

            FindEnemy(_spawner.EnemiesOnField);
            yield return null;
        }
    }

    private void SetObject(Bullet bullet, Vector3 spawnPoint)
    {
        bullet.gameObject.SetActive(true);
        bullet.gameObject.transform.position = spawnPoint;


        bullet.ChangeColor(_receiver.Layer, _receiver.Render.material);
        bullet.transform.rotation = transform.rotation;
        bullet.MoveToTarget(_currentTarget);
    }

    private void RotateToTarget()
    {
        Observable.EveryUpdate().Subscribe(_ => 
        {
            //if (_shellsAmount.Value <= 0 || _currentTarget == null)
            //{
            //    transform.rotation = Quaternion.Euler(_defaultRotation);
            //    _rotateDisposible.Clear();
            //}

            if(_currentTarget != null)
            {
                Quaternion currentRotation = transform.rotation;
                Quaternion targetRotation = Quaternion.LookRotation(transform.position - _currentTarget.transform.position);
                transform.localRotation = Quaternion.Slerp(currentRotation, targetRotation, Time.deltaTime * 200);
            }

        }).AddTo(_rotateDisposible);       
    }

    private void OnDisable()
    {
        _disposible.Clear();
        _attackDisposible.Clear();
        _rotateDisposible.Clear();
    }
}
