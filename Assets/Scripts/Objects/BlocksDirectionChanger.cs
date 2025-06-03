using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksDirectionChanger : MonoBehaviour
{
    private List<Block> _blocks = new List<Block>();

    private BlockGenerator _generator;

    private void Awake()
    {
        _generator = GetComponent<BlockGenerator>();
    }

    public void GetBlocks()
    {
        _blocks.Clear();

        StartCoroutine(AddToList());
    }

    private IEnumerator AddToList()
    {
        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < transform.childCount; i++)
        {
            _blocks.Add(transform.GetChild(i).GetComponent<Block>());
        }
    }

    public void DestroyBlock(Block block)
    {
        _blocks.Remove(block);
        Destroy(block);

        if (_blocks.Count == 0)
            _generator.GenerateGrid();

    }

    public void ChangeDirection()
    {    
        if(MoneyCounter.Instance.CoinAmount >= 3)
        {
            for (int i = 0; i < _blocks.Count; i++)
            {
                Vector3 rotate = _blocks[i].transform.eulerAngles;
                rotate.y += 180;
                _blocks[i].transform.rotation = Quaternion.Euler(rotate);
            }

            MoneyCounter.Instance.AddCoin(-3);
        }

        else
        {
            print("Недостаточно монет!");
        }
    }
}
