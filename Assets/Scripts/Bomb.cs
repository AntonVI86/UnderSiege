using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Bomb : Booster
{
    [SerializeField] private AudioClip _cancelSfx;
    [SerializeField] private AudioClip _errorSfx;
    [SerializeField] private Image _icon;
    [SerializeField] private float _radius;
    [SerializeField] private LayerMask _layer;
    [SerializeField] private ParticleSystem _explosionPrefab;

    private Button _button;
    private Slot _slot;
    private Inventory _inventory;

    private CompositeDisposable _disposible = new CompositeDisposable();

    [SerializeField] private bool _isActivate = false;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _slot = GetComponent<Slot>();
        _inventory = GetComponentInParent<Inventory>();
    }
    private void OnEnable()
    {
        _button.onClick.AddListener(ActivateBooster);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(ActivateBooster);
    }

    private void ActivateBooster()
    {
        _isActivate = true;
        _slot.ClearSlot();
        _inventory.gameObject.SetActive(false);
        Use();
    }

    public override void Use()
    {
        if (_isActivate)
        {
            Observable.EveryUpdate().Subscribe(_ =>
            {              
                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                    if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, _layer))
                    {
                        Collider[] colliders = Physics.OverlapSphere(hitInfo.point, _radius);
                        Instantiate(_explosionPrefab, new Vector3(hitInfo.point.x, hitInfo.point.y + 0.5f, hitInfo.point.z), Quaternion.Euler(-90,0,0));

                        foreach (var enemy in colliders)
                        {
                            if (enemy.TryGetComponent(out Enemy e))
                            {
                                e.EnemyDestroy();
                            }
                        }

                        SoundPlayer.Instance.PlaySound(UseSfx);

                        _isActivate = false;
                        _inventory.gameObject.SetActive(true);
                        _inventory.Save();
                        _disposible.Clear();
                    }
                    else
                    {
                        SoundPlayer.Instance.PlaySound(_errorSfx);
                    }
                }

                if (Input.GetMouseButtonDown(1))
                {
                    _disposible.Clear();
                    _isActivate = false;
                    _slot.Cancel();
                    _inventory.gameObject.SetActive(true);

                    SoundPlayer.Instance.PlaySound(_cancelSfx);
                }
            }).AddTo(_disposible);           
        }
    }
}
