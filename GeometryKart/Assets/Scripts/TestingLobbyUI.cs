using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class TestingLobbyUI : MonoBehaviour
{
    [SerializeField] private Button createGameButton;
    [SerializeField] private Button joinGameButton;

    private void Awake()
    {
        createGameButton.onClick.AddListener((() =>
        {
            NetworkManager.Singleton.StartHost();
            Loader.LoadNetwork(Loader.Scene.CharacterSelectScene);
        }));
        
        joinGameButton.onClick.AddListener((() =>
        {
            NetworkManager.Singleton.StartClient();
        }));
    }
}
