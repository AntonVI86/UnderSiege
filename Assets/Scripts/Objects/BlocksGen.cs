using System.Collections.Generic;
using UnityEngine;

public class BlocksGen : MonoBehaviour
{
    public enum Direction { Up, Right, Down, Left }

    public class BlockData
    {
        public Vector2 Position;
        public Direction Direction;
        public float Length;
        public int Row => Mathf.FloorToInt(Position.y);
        public int Col => Mathf.FloorToInt(Position.x);
    }

    public int blocksToGenerate = 10;
    public GameObject shortBlockPrefab;
    public GameObject longBlockPrefab;
    public float cellSize = 1.0f;

    private List<BlockData> blocks = new List<BlockData>();
    private Dictionary<int, List<Direction>> rowDirections = new Dictionary<int, List<Direction>>();
    private Dictionary<int, List<Direction>> colDirections = new Dictionary<int, List<Direction>>();

    private void Start()
    {
        GenerateBlocks();
    }

    private void GenerateBlocks()
    {
        GenerateFirstBlock();

        for (int i = 1; i < blocksToGenerate; i++)
        {
            GenerateNextBlock();
        }

        VisualizeBlocks();
    }

    private void GenerateFirstBlock()
    {
        BlockData firstBlock = new BlockData
        {
            Position = Vector2.zero,
            Direction = (Direction)Random.Range(0, 4),
            Length = Random.Range(0, 2) == 0 ? cellSize : cellSize * 1.5f
        };

        blocks.Add(firstBlock);
        AddToDirectionDictionaries(firstBlock);
    }

    private void GenerateNextBlock()
    {
        BlockData lastBlock = blocks[blocks.Count - 1];
        Vector2 newPosition = lastBlock.Position + DirectionToVector(lastBlock.Direction) * lastBlock.Length;
        float newLength = Random.Range(0, 2) == 0 ? cellSize : cellSize * 1.5f;

        List<Direction> possibleDirections = new List<Direction>();
        Direction oppositeToPrev = GetOppositeDirection(lastBlock.Direction);

        // Проверка всех возможных направлений
        for (int i = 0; i < 4; i++)
        {
            Direction testDir = (Direction)i;
            if (testDir == oppositeToPrev) continue;

            if (IsDirectionValidForPosition(testDir, newPosition))
            {
                possibleDirections.Add(testDir);
            }
        }

        // Если нет подходящих направлений, используем любое (кроме противоположного)
        Direction newDirection = possibleDirections.Count > 0
            ? possibleDirections[Random.Range(0, possibleDirections.Count)]
            : GetRandomDirectionExcept(oppositeToPrev);

        BlockData newBlock = new BlockData
        {
            Position = newPosition,
            Direction = newDirection,
            Length = newLength
        };

        blocks.Add(newBlock);
        AddToDirectionDictionaries(newBlock);
    }

    private bool IsDirectionValidForPosition(Direction dir, Vector2 position)
    {
        int row = Mathf.FloorToInt(position.y);
        int col = Mathf.FloorToInt(position.x);
        Direction opposite = GetOppositeDirection(dir);

        // Проверка строки
        if (rowDirections.TryGetValue(row, out List<Direction> rowDirs))
        {
            if (IsHorizontal(dir) && rowDirs.Contains(opposite))
                return false;
        }

        // Проверка столбца
        if (colDirections.TryGetValue(col, out List<Direction> colDirs))
        {
            if (!IsHorizontal(dir) && colDirs.Contains(opposite))
                return false;
        }

        return true;
    }

    private void AddToDirectionDictionaries(BlockData block)
    {
        int row = block.Row;
        int col = block.Col;

        // Добавление в словарь строк
        if (!rowDirections.ContainsKey(row))
            rowDirections[row] = new List<Direction>();
        rowDirections[row].Add(block.Direction);

        // Добавление в словарь столбцов
        if (!colDirections.ContainsKey(col))
            colDirections[col] = new List<Direction>();
        colDirections[col].Add(block.Direction);
    }

    private Direction GetRandomDirectionExcept(Direction excluded)
    {
        List<Direction> directions = new List<Direction>
            { Direction.Up, Direction.Right, Direction.Down, Direction.Left };
        directions.Remove(excluded);
        return directions[Random.Range(0, directions.Count)];
    }

    private Direction GetOppositeDirection(Direction dir)
    {
        switch (dir)
        {
            case Direction.Up: return Direction.Down;
            case Direction.Right: return Direction.Left;
            case Direction.Down: return Direction.Up;
            case Direction.Left: return Direction.Right;
            default: return dir;
        }
    }

    private Vector2 DirectionToVector(Direction dir)
    {
        switch (dir)
        {
            case Direction.Up: return Vector2.up;
            case Direction.Right: return Vector2.right;
            case Direction.Down: return Vector2.down;
            case Direction.Left: return Vector2.left;
            default: return Vector2.zero;
        }
    }

    private bool IsHorizontal(Direction dir)
    {
        return dir == Direction.Left || dir == Direction.Right;
    }

    private void VisualizeBlocks()
    {
        foreach (BlockData block in blocks)
        {
            GameObject prefab = block.Length > cellSize * 1.25f
                ? longBlockPrefab : shortBlockPrefab;

            GameObject blockObj = Instantiate(
                prefab,
                block.Position,
                Quaternion.identity
            );

            // Поворот объекта в соответствии с направлением
            float angle = 0;
            switch (block.Direction)
            {
                case Direction.Right: angle = 0; break;
                case Direction.Up: angle = 90; break;
                case Direction.Left: angle = 180; break;
                case Direction.Down: angle = 270; break;
            }
            blockObj.transform.Rotate(0, 0, angle);
        }
    }
}