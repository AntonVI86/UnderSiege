using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBlockGenerator : MonoBehaviour
{
    [SerializeField] private int _xSize;
    [SerializeField] private int _ySize;

    [SerializeField] private List<Block> _blocksPrefab = new List<Block>();

    [SerializeField] private float _startPointX;
    [SerializeField] private float _startPointZ;

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        Block block = null;
        float offset = 0.5f;

        for (int z = 0; z < _xSize; z++)
        {
            for (int x = 0; x < _ySize; x++)
            {
                int randomBlockNumber = Random.Range(0, _blocksPrefab.Count);

                block = Instantiate(_blocksPrefab[randomBlockNumber], new Vector3(_startPointX, transform.position.y, _startPointZ), Quaternion.identity);
                //SetDirection(block);

                _startPointX += block.transform.localScale.x + offset;
            }

            _startPointZ += block.transform.localScale.z + offset;
            _startPointX = 0f;
        }
    }


    private bool TryPlaceDoubleBlock()
    {
        return true;
    }

    private void SetDirection(Block block)
    {
        Quaternion[] directions = 
        {
            Quaternion.Euler(0, 0, 0),
            Quaternion.Euler(0, 90, 0),
            Quaternion.Euler(0, 180, 0),
            Quaternion.Euler(0, 270, 0)
        };

        block.transform.rotation = directions[Random.Range(0, directions.Length)];
    }
}
