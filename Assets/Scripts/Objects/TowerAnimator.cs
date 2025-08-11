using UnityEngine;

public class TowerAnimator : MonoBehaviour
{
    [SerializeField] private ParticleSystem _attackVfx;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void AttackEffect()
    {
        _animator.Play("TowerAttack");
        _attackVfx.Play();
    }
}
