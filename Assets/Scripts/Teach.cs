using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Teach : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private RectTransform _arrow;
    
    [SerializeField] private string[] _messages;
    [SerializeField] private RectTransform[] _arrowPositions;
    [SerializeField] private float[] _rotationEuler;

    [SerializeField] private TMP_Text _message;
    [SerializeField] private Button _ok;

    private int _index = -1;

    private void OnEnable()
    {
        _ok.onClick.AddListener(OnClickButton);
    }

    private void Start()
    {
        if(PlayerPrefs.HasKey("Level") == false)
        {
            Time.timeScale = 0;
            _panel.SetActive(true);
            _index++;
            _message.text = Lean.Localization.LeanLocalization.GetTranslationText(_messages[_index]);
        }
    }

    private void OnClickButton()
    {
        if(_index < _messages.Length -1)
        {
            _index++;
            _message.text = Lean.Localization.LeanLocalization.GetTranslationText(_messages[_index]); ;
            _arrow.anchoredPosition = _arrowPositions[_index].anchoredPosition;
            _arrow.localRotation = Quaternion.Euler(0,0,_rotationEuler[_index]);
            return;
        }

        _panel.SetActive(false);
        Time.timeScale = 1;
    }

    private void OnDisable()
    {
        _ok.onClick.RemoveListener(OnClickButton);
    }
}
