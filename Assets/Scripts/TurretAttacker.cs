using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using UnityEngine.Events;

public class TurretAttacker : MonoBehaviour
{
    [SerializeField] private EnemySpawner _spawner;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private float _delayToAttack;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private ShellReceiver _receiver;

    public event UnityAction<int> ShellAmountChanged;
    private IntReactiveProperty _shellsAmount = new IntReactiveProperty();

    private CompositeDisposable _disposible = new CompositeDisposable();
    private CompositeDisposable _attackDisposible = new CompositeDisposable();
    private CompositeDisposable _rotateDisposible = new CompositeDisposable();

    private Enemy _currentTarget;

    private Coroutine _coroutine;

    private void Start()
    {
        _shellsAmount.Subscribe(_ => 
        {
            if(_shellsAmount.Value <= 0)
            {
                _receiver.ResetTurret();
                _attackDisposible.Clear();
            }
        }).AddTo(_disposible);
    }

    public void GetShells(int value)
    {
        _shellsAmount.Value += value;

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
        foreach (var enemy in enemies)
        {
            if(CompareFirstCharacters(enemy.gameObject.name, _receiver.Render.material.name))
            {
                _currentTarget = enemy;
            }
        }
    }

    private IEnumerator Attack()
    {
        FindEnemy(_spawner.EnemiesOnField);

        while (_shellsAmount.Value > 0)
        {           
            if (_currentTarget != null)
            {
                RotateToTarget();

                yield return new WaitForSeconds(0.8f);
                
                Bullet newBullet = Instantiate(_bulletPrefab, _attackPoint.position, Quaternion.identity);

                newBullet.ChangeColor(_receiver.Layer, _receiver.Render.material);
                newBullet.transform.rotation = transform.rotation;
                newBullet.MoveToTarget(_currentTarget);

                _shellsAmount.Value--;

                ShellAmountChanged?.Invoke(_shellsAmount.Value);

            }

            FindEnemy(_spawner.EnemiesOnField);

            yield return null;
        }
    }

    private void RotateToTarget()
    {
        Observable.EveryUpdate().Subscribe(_ => 
        {
            if (_shellsAmount.Value <= 0 || _currentTarget == null)
                _rotateDisposible.Clear();

            if(_currentTarget != null)
            {
                Quaternion currentRotation = transform.rotation;
                Quaternion targetRotation = Quaternion.LookRotation(transform.position - _currentTarget.transform.position);
                transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, Time.deltaTime * 200);
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
