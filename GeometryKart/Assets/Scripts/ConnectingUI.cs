using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectingUI : MonoBehaviour
{
    private void Start()
    {
        RaceGameMultiplayer.Instance.OnTryingToJoinGame += RaceGameMultiplayer_OnTryingToJoinGame;
        RaceGameMultiplayer.Instance.OnFailedToJoinGame += RaceGameMultiplayer_OnFailedToJoinGame;
        Hide( );
    }

    private void RaceGameMultiplayer_OnFailedToJoinGame(object sender, EventArgs e)
    {
        Hide();
    }

    private void RaceGameMultiplayer_OnTryingToJoinGame(object sender, EventArgs e)
    {
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    
    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        RaceGameMultiplayer.Instance.OnTryingToJoinGame -= RaceGameMultiplayer_OnTryingToJoinGame;
        RaceGameMultiplayer.Instance.OnFailedToJoinGame -= RaceGameMultiplayer_OnFailedToJoinGame;
    }
}
