using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private List<Block> _blockPrefabs = new List<Block>();

    [SerializeField] private Cell _cellPrefab;

    [SerializeField] private int _sizeX;
    [SerializeField] private int _sizeZ;

    private float _startPositionX = 0;
    private float _startPositionZ = 0;

    private float _offset = 0.5f;

    private List<Cell> _cells = new List<Cell>();
    private List<Block> _blocks = new List<Block>();

    private Quaternion[] _directions =
        {
            Quaternion.Euler(0, 0, 0),
            Quaternion.Euler(0, 90, 0),
            Quaternion.Euler(0, 180, 0),
            Quaternion.Euler(0, 270, 0)
        };

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        Cell newCell = null;

        for (int x = 0; x < _sizeX; x++)
        {
            for (int z = 0; z < _sizeZ; z++)
            {
                newCell = Instantiate(_cellPrefab, new Vector3(_startPositionX, 0, _startPositionZ), Quaternion.Euler(90,0,0));
                newCell.transform.SetParent(transform);
                _cells.Add(newCell);

                _startPositionZ += newCell.Size + 0.5f;
            }

            _startPositionZ = 0;
            _startPositionX += newCell.Size + 0.5f;
        }

        PlaceBlocks();
    }

    private void PlaceBlocks()
    {       
        for (int i = 0; i < _cells.Count; i++)
        {
            if(_cells[i].IsOccupied == false)
            {
                int blockNumber = Random.Range(0, _blockPrefabs.Count);

                Block newBlock = Instantiate(_blockPrefabs[blockNumber]);

                newBlock.transform.localRotation = _directions[Random.Range(0, _directions.Length)];

                _blocks.Add(newBlock);

                if(newBlock.transform.localScale.z > _cells[i].Size)
                {
                    PlaceDoubleBlock(newBlock, i, 1, 0, 2, 0, _offset);

                    PlaceDoubleBlock(newBlock, i, _sizeX, 1, 3, _offset, 0);                   
                }
                else
                {
                    SetPosition(newBlock, _cells[i]);
                }               
            }
        }
    }

    private void PlaceDoubleBlock(Block block, int cellNumber, int nextCellNumber, int firstEulerNumber, int secondEulerNumber, float offsetX, float offsetZ)
    {
        int startNumberOfArray = 6;

        if (block.transform.localRotation == _directions[firstEulerNumber] || block.transform.localRotation == _directions[secondEulerNumber])
        {
            if (cellNumber + nextCellNumber < _cells.Count && _cells[cellNumber + nextCellNumber].IsOccupied == false)
            {
                if(cellNumber != _sizeX - 1 && cellNumber != _sizeZ - 1)
                {
                    block.transform.SetParent(_cells[cellNumber].transform);
                    block.transform.position = new Vector3(_cells[cellNumber].transform.position.x + offsetX, 0, _cells[cellNumber].transform.position.z + offsetZ);
                    _cells[cellNumber + nextCellNumber].Occupy();
                }
                else
                {
                    Destroy(block.gameObject);
                    _blocks.Remove(block);

                    int blockNumber = Random.Range(startNumberOfArray, _blockPrefabs.Count);

                    block = Instantiate(_blockPrefabs[blockNumber]);

                    block.transform.localRotation = _directions[Random.Range(0, _directions.Length)];

                    SetPosition(block, _cells[cellNumber]);
                }
            }
            else
            {
                Destroy(block.gameObject);
                _blocks.Remove(block);

                int blockNumber = Random.Range(startNumberOfArray, _blockPrefabs.Count);

                block = Instantiate(_blockPrefabs[blockNumber]);

                block.transform.localRotation = _directions[Random.Range(0, _directions.Length)];

                SetPosition(block, _cells[cellNumber]);              
            }
        }
    }

    private void SetPosition(Block block, Cell cell)
    {
        block.transform.SetParent(cell.transform);
        block.transform.position = cell.transform.position;
        _blocks.Add(block);
    }

    private void ChangeDirection()
    {

    }

    private void ClearField()
    {
        foreach (var block in _blocks)
        {
            Destroy(block.gameObject);
            _blocks.Remove(block);
        }       
    }
}
