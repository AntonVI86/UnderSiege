using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksDirectionChanger : MonoBehaviour
{
    private List<Block> _blocks = new List<Block>();

    public void GetBlocks()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            _blocks.Add(transform.GetChild(i).GetComponent<Block>());
        }
    }

    public void DestroyBlock(Block block)
    {
        _blocks.Remove(block);
        Destroy(block, 0.5f);
    }

    public void ChangeDirection()
    {       
        for (int i = 0; i < _blocks.Count; i++)
        {
            Vector3 rotate = _blocks[i].transform.eulerAngles;
            rotate.y += 180;
            _blocks[i].transform.rotation = Quaternion.Euler(rotate);
        }
    }
}
