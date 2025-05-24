using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] private Transform _blocksStartPoint;

    [SerializeField] private int _gridWidth;
    [SerializeField] private int _gridHeight;

    [SerializeField] private List<Block> _singleCellPrefabs;
    [SerializeField] private List<Block> _doubleCellPrefabs;

    private bool[,] _occupiedCells;
    
    private Dictionary<Vector2Int, Direction> _cellDirections = new Dictionary<Vector2Int, Direction>();

    private enum Direction { Up, Right, Down, Left }

    public void GenerateGrid()
    {
        ClearField();
        InitializeGrid();
        FillGrid();

        transform.rotation = Quaternion.Euler(0,45,0);
        transform.position = _blocksStartPoint.position;
    }

    private void InitializeGrid()
    {
        _occupiedCells = new bool[_gridWidth, _gridHeight];
        _cellDirections.Clear();
    }

    private void FillGrid()
    {
        for (int y = 0; y < _gridHeight; y++)
        {
            for (int x = 0; x < _gridWidth; x++)
            {
                if (_occupiedCells[x, y]) continue;

                // Пробуем разместить двухячеечный блок
                if (TryPlaceDoubleCell(x, y))
                {
                    continue;
                }

                // Если не получилось - размещаем одноячеечный
                PlaceSingleCell(x, y);
            }
        }
    }

    private bool TryPlaceDoubleCell(int x, int y)
    {
        // Получаем доступные направления для двух ячеек
        List<Direction> availableDirections = GetValidDirections(x, y, true);

        if (availableDirections.Count == 0) return false;

        // Выбираем случайное направление
        Direction dir = availableDirections[Random.Range(0, availableDirections.Count)];
        Vector2Int secondCell = GetSecondCell(x, y, dir);

        // Проверяем выход за границы
        if (IsCellValid(secondCell.x, secondCell.y) == false) return false;

        // Проверяем занятость
        if (_occupiedCells[secondCell.x, secondCell.y]) return false;

        // Размещаем блок
        PlaceBlock(x, y, dir, false);
        _occupiedCells[x, y] = true;
        _occupiedCells[secondCell.x, secondCell.y] = true;
        return true;
    }

    private void PlaceSingleCell(int x, int y)
    {
        List<Direction> availableDirections = GetValidDirections(x, y, false);
        
        Direction dir = availableDirections.Count > 0 ?
            availableDirections[Random.Range(0, availableDirections.Count)] :
            Direction.Up;

        PlaceBlock(x, y, dir, true);
        _occupiedCells[x, y] = true;
    }

    private List<Direction> GetValidDirections(int x, int y, bool isDouble)
    {
        List<Direction> validDirections = new List<Direction>();

        foreach (Direction dir in System.Enum.GetValues(typeof(Direction)))
        {
            if (IsDirectionValid(x, y, dir, isDouble))
            {
                validDirections.Add(dir);
            }
        }

        return validDirections;
    }

    private bool IsDirectionValid(int x, int y, Direction dir, bool isDouble)
    {
        // Проверка на выход за границы
        if (isDouble)
        {
            Vector2Int secondCell = GetSecondCell(x, y, dir);

            if (IsCellValid(secondCell.x, secondCell.y) == false) return false;
        }

        // Проверка направлений соседей
        foreach (var neighbor in GetNeighbors(x, y, dir, isDouble))
        {
            if (_cellDirections.ContainsKey(neighbor.Key))
            {
                Direction oppositeDir = GetOppositeDirection(neighbor.Value);

                if (_cellDirections[neighbor.Key] == oppositeDir) return false;
            }
        }

        return true;
    }

    private Vector2Int GetSecondCell(int x, int y, Direction dir)
    {
        return dir switch
        {
            Direction.Up => new Vector2Int(x, y - 1),
            Direction.Right => new Vector2Int(x - 1, y),
            Direction.Down => new Vector2Int(x, y + 1),
            Direction.Left => new Vector2Int(x + 1, y),
            _ => new Vector2Int(x, y)
        };
    }

    private Dictionary<Vector2Int, Direction> GetNeighbors(int x, int y, Direction dir, bool isDouble)
    {
        Dictionary<Vector2Int, Direction> neighbors = new Dictionary<Vector2Int, Direction>();

        // Добавляем соседей для текущей ячейки
        AddNeighbor(x + 1, y, Direction.Left);
        AddNeighbor(x - 1, y, Direction.Right);
        AddNeighbor(x, y + 1, Direction.Down);
        AddNeighbor(x, y - 1, Direction.Up);

        // Если двухячеечный блок - добавляем соседей для второй ячейки
        if (isDouble)
        {
            Vector2Int secondCell = GetSecondCell(x, y, dir);
            AddNeighbor(secondCell.x + 1, secondCell.y, Direction.Left);
            AddNeighbor(secondCell.x - 1, secondCell.y, Direction.Right);
            AddNeighbor(secondCell.x, secondCell.y + 1, Direction.Down);
            AddNeighbor(secondCell.x, secondCell.y - 1, Direction.Up);
        }

       void AddNeighbor(int nx, int ny, Direction ndir)
        {
            if (IsCellValid(nx, ny))
            {
                neighbors.Add(new Vector2Int(nx, ny), ndir);
            }
        }

        return neighbors;
    }

    private Direction GetOppositeDirection(Direction dir)
    {
        return dir switch
        {
            Direction.Up => Direction.Down,
            Direction.Right => Direction.Left,
            Direction.Down => Direction.Up,
            Direction.Left => Direction.Right,
            _ => Direction.Up
        };
    }

    private bool IsCellValid(int x, int y)
    {
        return x >= 0 && x < _gridWidth && y >= 0 && y < _gridHeight;
    }

    private void PlaceBlock(int x, int y, Direction dir, bool isSingle)
    {
        List<Block> prefabs = isSingle ? _singleCellPrefabs : _doubleCellPrefabs;
        Block prefab = prefabs[Random.Range(0, prefabs.Count)];
        Quaternion rotation = Quaternion.Euler(0, ((int)dir) * 90, 0);

        Vector3 position = new Vector3(x, transform.position.y, y);

        if (isSingle == false)
        {
            Vector2Int offset = GetSecondCell(0, 0, dir);
            position += new Vector3(offset.x * 0.5f, 0, offset.y * 0.5f);
        }

        Block block = Instantiate(prefab, position, rotation, transform);
        _cellDirections[new Vector2Int(x, y)] = dir;
    }

    private void ClearField()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.position = Vector3.zero;
    }
}
