using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private float _size = 0.5f;

    private bool _isOccupied = false;
    private Block _block;

    public float Size => _size;
    public bool IsOccupied => _isOccupied;

    public void Occupy()
    {
        _isOccupied = true;
    }
    public void Clear()
    {
        _isOccupied = false;
    }
}
