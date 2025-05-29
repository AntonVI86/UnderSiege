using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class Block : MonoBehaviour, IPointerClickHandler, IMovable
{
    [SerializeField] private LayerMask _layer;
    [SerializeField] private LayerMask _wallLayer;
    [SerializeField] private int _layerForReceived;

    [SerializeField] private float _speed;

    [SerializeField] private int _minShellValue;
    [SerializeField] private int _maxShellValue;

    private Renderer _renderer;

    private CompositeDisposable _moveDisposible = new CompositeDisposable();
    private BlocksDirectionChanger _changer;

    public Renderer Render => _renderer;

    private void Awake()
    {
        _changer = GetComponentInParent<BlocksDirectionChanger>();
        _renderer = GetComponent<Renderer>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Move();
    }

    public void Move()
    {
        Observable.EveryUpdate().Subscribe(_ => 
        {
            bool isHit = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), 1.2f, _layer);

            if(isHit == false)
            {
                MovingToTarget();
            }

            if(isHit)
                _moveDisposible.Clear();

        }).AddTo(_moveDisposible);
    }

    public void MovingToTarget()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 0.8f))
        {
            if(hit.transform.TryGetComponent(out Wall wall))
            {
                Vector3 rotate = transform.eulerAngles;
                rotate.y = wall.Angle;
                transform.rotation = Quaternion.Euler(rotate);
            }

            if(hit.transform.TryGetComponent(out ShellReceiver turret))
            {
                if (turret.IsBusy)
                {
                    if(CompareFirstCharacters(turret.Render.material.name,_renderer.material.name))
                    {
                        int shellValue = Random.Range(_minShellValue, _maxShellValue);

                        turret.GetShells(_layerForReceived, _renderer.material, shellValue);
                        _moveDisposible.Clear();
                        _changer.DestroyBlock(this);
                        Destroy(gameObject);
                    }
                }

                if(turret.IsBusy == false)
                {
                    int shellValue = Random.Range(_minShellValue, _maxShellValue);

                    turret.GetShells(_layerForReceived, _renderer.material, shellValue);
                    _renderer.material.name = _renderer.material.name;
                    _moveDisposible.Clear();
                    _changer.DestroyBlock(this);
                    Destroy(gameObject);
                }

            }
        }
    }
    private bool CompareFirstCharacters(string first, string second)
    {
        if (first.Substring(0, 5) == second.Substring(0,5))
        {
            return true;
        }

        return false;
    }
}
