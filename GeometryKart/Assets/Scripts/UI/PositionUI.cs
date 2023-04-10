using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class PositionUI : NetworkBehaviour
{

    [SerializeField] private TextMeshProUGUI positionText;


    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
        {
            gameObject.SetActive(false);
        }
    }

    private void Awake()
    {
        RaceGameMultiplayer.Instance.OnPlayerDataNetworkListChanged += RaceGameMultiplayer_OnPlayerDataNetworkListChanged;
    }

    private void RaceGameMultiplayer_OnPlayerDataNetworkListChanged(object sender, EventArgs e)
    {
        var playerData = RaceGameMultiplayer.Instance.GetPlayerDataFromClientId(OwnerClientId);

        UpdatePositionText(playerData.position);
    }

    public void UpdatePositionText(int position)
    {
        positionText.text = position + ".";
    }
    
    private void Show()
    {
        gameObject.SetActive(true);
    }
    
    private void Hide()
    {
        gameObject.SetActive(false);
    }

    public override void OnDestroy()
    {
        RaceGameMultiplayer.Instance.OnPlayerDataNetworkListChanged += RaceGameMultiplayer_OnPlayerDataNetworkListChanged;
    }
}
