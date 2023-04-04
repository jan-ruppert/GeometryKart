using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    private const float STANDARD_TIME_SCALE = 1f;
    
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    private void Awake()
    {
        playButton.onClick.AddListener((() =>
        {
            Loader.Load(Loader.Scene.LobbyScene);
        }));
        
        quitButton.onClick.AddListener((() =>
        {
            Application.Quit();
        }));

        Time.timeScale = STANDARD_TIME_SCALE;
    }
}
