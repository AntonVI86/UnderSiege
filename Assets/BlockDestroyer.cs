using UnityEngine;

public class BlockDestroyer : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out Block block))
        {
            Destroy(block);
        }
    }
}
