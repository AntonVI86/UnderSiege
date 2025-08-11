using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text _timeVisual;
    [SerializeField] private Image _timerVisual;

    public void Launch(float time, ParticleSystem ps)
    {
        _timerVisual.gameObject.SetActive(true);

        StartCoroutine(CountDown(time, ps));
    }

    private IEnumerator CountDown(float time, ParticleSystem ps)
    {
        while(time > 0)
        {
            time -= Time.deltaTime;
            _timeVisual.text = Mathf.Round(time).ToString();
            yield return null;
        }

        ps.Stop();
        _timerVisual.gameObject.SetActive(false);
    }
}
