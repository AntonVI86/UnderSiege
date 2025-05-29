using UnityEngine;

public abstract class Panel: MonoBehaviour
{
    public void Show() => gameObject.SetActive(true);
    public void Hide() => gameObject.SetActive(false);
    public abstract void OnClickNextButton();
    public abstract void OnClickShowRewardButton();
}
