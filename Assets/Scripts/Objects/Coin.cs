using UnityEngine;
using UnityEngine.EventSystems;

public class Coin : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private AudioClip _pickUpSfx;
    [SerializeField] private AudioClip _dropOutSfx;

    public void PlayDropOutSound()
    {
        SoundPlayer.Instance.PlaySound(_dropOutSfx);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        MoneyCounter.Instance.AddCoin(1);
        SoundPlayer.Instance.PlaySound(_pickUpSfx);
        Destroy(gameObject);
    }
}
