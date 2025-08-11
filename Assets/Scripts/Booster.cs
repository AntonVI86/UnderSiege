using UnityEngine;

public abstract class Booster : MonoBehaviour
{
    [SerializeField] protected AudioClip UseSfx;
    public abstract void Use();
}
