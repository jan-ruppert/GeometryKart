using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectPlayer : MonoBehaviour
{
    [SerializeField] private int playerIndex;
    [SerializeField] private GameObject readyGameObject;
    [SerializeField] private Button kickButton;

    private void Awake()
    {
        kickButton.onClick.AddListener((() =>
        {
            PlayerData playerData = RaceGameMultiplayer.Instance.GetPlayerDataFromPlayerIndex(playerIndex);
            RaceGameMultiplayer.Instance.KickPlayer(playerData.clientId);
        }));
    }

    private void Start()
    {
        RaceGameMultiplayer.Instance.OnPlayerDataNetworkListChanged += RaceGameMultiplayer_OnPlayerDataNetworkListChanged;
        
        CharacterSelectReady.Instance.OnReadyChanged += CharacterSelectReady_OnReadyChanged;
        
        kickButton.gameObject.SetActive(NetworkManager.Singleton.IsServer);
        
        UpdatePlayer();
    }

    private void CharacterSelectReady_OnReadyChanged(object sender, EventArgs e)
    {
        UpdatePlayer();
    }

    private void RaceGameMultiplayer_OnPlayerDataNetworkListChanged(object sender, EventArgs e)
    {
        UpdatePlayer();
    }

    private void UpdatePlayer()
    {
        if (RaceGameMultiplayer.Instance.IsPlayerIndexConnected(playerIndex))
        {
            Show();

            PlayerData playerData = RaceGameMultiplayer.Instance.GetPlayerDataFromPlayerIndex(playerIndex);
            readyGameObject.SetActive(CharacterSelectReady.Instance.IsPlayerReady(playerData.clientId));
        }
        else
        {
            Hide();
        }
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
        RaceGameMultiplayer.Instance.OnPlayerDataNetworkListChanged -= RaceGameMultiplayer_OnPlayerDataNetworkListChanged;
    }
}
