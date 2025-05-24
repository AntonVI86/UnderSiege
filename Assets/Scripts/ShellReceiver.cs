using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShellReceiver : MonoBehaviour
{
    [SerializeField] private BlocksDirectionChanger _changer;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private TurretAttacker _attacker;

    public event UnityAction<Color,int> Received;

    private Material _default;

    private bool _isBusy = false;


    private int _layer;

    public bool IsBusy => _isBusy;
    public Renderer Render => _renderer;
    public LayerMask Layer => _layer;

    private void Start()
    {
        _default = _renderer.material;
    }

    public void GetShells(int layer, Material material, int value)
    {
        _layer = layer;
        _renderer.material = material;
        _attacker.GetShells(value);
        _isBusy = true;

        Received?.Invoke(material.color, value);
    }

    public void ResetTurret()
    {
        _renderer.material = _default;
        _isBusy = false;
        _layer = 0;
    }
}
