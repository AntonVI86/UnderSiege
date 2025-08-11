using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterstitialShower : MonoBehaviour
{
    private int _gameOverCount = 0;

    private void Start()
    {
        _gameOverCount = 0;

        if (PlayerPrefs.HasKey("GameOverCount"))
        {
            _gameOverCount = PlayerPrefs.GetInt("GameOverCount");
        }

        if(_gameOverCount > 0)
        {
            Agava.VKGames.Interstitial.Show();
        }
    }
}
