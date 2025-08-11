using UnityEngine;
using UnityEngine.UI;

public class FreezeBooster : Booster
{
    [SerializeField] private ParticleSystem _freezeVfx;

    private EnemySpawner _spawner;
    private Timer _timer;
    private Slot _slot;
    private Button _button; 

    private float _time = 7f;

    private void Awake()
    {
        _spawner = GetComponentInParent<HealthGetter>().Spawner;
        _timer = GetComponentInParent<HealthGetter>().FreezeTimer;
        _slot = GetComponent<Slot>();
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(Use);
    }

    public override void Use()
    {
        foreach (Enemy enemy in _spawner.EnemiesOnField)
        {
            enemy.ChangeSpeed(_time);
        }

        _freezeVfx.Play();
        _timer.Launch(_time, _freezeVfx);
        _slot.ClearSlot();

        SoundPlayer.Instance.PlaySound(UseSfx);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(Use);
    }
}
