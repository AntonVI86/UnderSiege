using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Coin : MonoBehaviour, IPointerClickHandler
{
    public event UnityAction Picked;

    public void OnPointerClick(PointerEventData eventData)
    {
        MoneyCounter.Instance.AddCoin(1);
        Destroy(gameObject);
    }
}
